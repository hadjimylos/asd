--FIXES / NOTES
--===================================================================
--Export/Application Type  = has only dummy data in our database 
--Migration Query  
--=============================================
use [pmo]

--First Create below Function that splits users that are comma separated 
--===============================================================
CREATE FUNCTION [dbo].[udf-Str-Parse] (@String nvarchar(max),@Delimiter nvarchar(10))
Returns Table 
As
Return (  
    Select RetSeq = Row_Number() over (Order By (Select null))
          ,RetVal = LTrim(RTrim(B.i.value('(./text())[1]', 'nvarchar(max)')))
    From (Select x = Cast('<x>'+ Replace(@String,@Delimiter,'</x><x>')+'</x>' as xml).query('.')) as A 
    Cross Apply x.nodes('x') AS B(i)
);
--Then Run below queries 
--===============================================================

--step 1  ADD USERS and Team Members 
SELECT  res.USER_ID , res.PROJECT_ID , res.SEGMENT_ID , segment.SEGMENT_CAPTION ,res.Gate_ID 
into #temp
FROM USSFA1SRPTPCH02.ITTCo_PMO_PROD.dbo.PROJECT_GATE_RESOURCE_DETAILS res
left join USSFA1SRPTPCH02.ITTCo_PMO_PROD.dbo.PROJECT_DETAILS  detail on detail.PROJECT_ID = res.PROJECT_ID 
inner join USSFA1SRPTPCH02.ITTCo_PMO_PROD.dbo.VBPD_RESOURCE_SEGMENT_MASTER segment on segment.SEGMENT_ID = res.SEGMENT_ID
inner join (
select project_ID, Max(res.GATE_ID) Gate_ID  FROM USSFA1SRPTPCH02.ITTCo_PMO_PROD.dbo.PROJECT_GATE_RESOURCE_DETAILS res 
where res.USER_ID is not null
Group By res.PROJECT_ID
) as table1 
on table1.PROJECT_ID =  detail.PROJECT_ID and table1.Gate_ID = res.GATE_ID
where detail.UI_STATUS = 'Open' and res.USER_ID is not null	 -- and res.Project_ID = '1479'  
order by res.Project_ID

Select  Distinct  GETDATE() CreateDate,  PROJECT_ID , USER_ID = B.RetVal , Lower(Replace(RTRIM(SEGMENT_CAPTION),' ','-'))SEGMENT_CAPTION
 into #temp1 From  #temp A
 Cross Apply [dbo].[udf-Str-Parse](A.USER_ID,',') B 

--ADD USERS THAT ARE project Team Members Only 
 insert into Users (CreateDate, ModifiedByUser, NetworkUsername, RoleId , DisplayName)
 Select Distinct  t.CreateDate ,'system' , Lower(USER_ID) as NetworkName , 2 ,users.Last_name+', '+users.First_Name From  #temp1 t
 left join USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[USERS] users on users.[USERID] = t.User_ID
 
 Drop Table #temp
 
 --step 2 ADD Project (master data)
 
SET IDENTITY_INSERT Projects ON
insert Into Projects (Id , CreateDate , ModifiedByUser,Name)
select details.PROJECT_ID , details.DATE_ENTERED, 'system' , details.PROGRAM_OR_PROJECT_NAME
from [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS details
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PRODUCT_LINE_MASTER product on  product.PRODUCT_LINE_ID = details.PRODUCT_LINE 
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_CLASSIFICATION_MASTER class on class.PROJECT_CLASSIFICATION_ID = details.PROJECT_CLASSIFICATION
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.TYPE_OF_PRODUCT_MASTER typeOfProduct on typeOfProduct.TOP_CD = details.TYPE_OF_PRODUCT
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.VBPD_WORKBOOK_MASTER workbook on workbook.[VBPD_WORKBOOK_ID] = details.VBPD_WORKBOOK_ID
where details.UI_STATUS='Open' order by details.PROJECT_ID
SET IDENTITY_INSERT Projects OFF
GO



--step 3  create all temp tables that projectDetail needs
 
 --------TEMP----------- Project Classification ([PROJECT_CLASSIFICATION_MASTER]) 
SELECT   tags.Id, p.PROJECT_ID  ,p.PROJECT_CLASSIFICATION 
into #tempProjClassification 
FROM [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.[PROJECT_DETAILS] p 
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.[PROJECT_CLASSIFICATION_MASTER] c on c.PROJECT_CLASSIFICATION_ID = p.PROJECT_CLASSIFICATION
inner join (select * from tags where TagCategoryId = 5) as Tags on tags.Name =  c.PROJECT_CLASSIFICATION
where p.UI_STATUS='Open' order by p.PROJECT_ID 

--------TEMP----------- Project Category ([TYPE_OF_PRODUCT_MASTER]) 
SELECT case when (tags.Id is null) then 222 else tags.Id END Id  
       ,p.PROJECT_ID  ,p.TYPE_OF_PRODUCT
into #tempProjCategory 
From [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.[PROJECT_DETAILS] p 
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.[TYPE_OF_PRODUCT_MASTER] c on c.TOP_CD = p.TYPE_OF_PRODUCT
left join (select * from tags where TagCategoryId = 4) as Tags on tags.Name = C.[TOP_DESCRIPTION]
where p.UI_STATUS='Open' order by p.PROJECT_ID 

--------TEMP----------- Design Authority ([PROJECT_DETAILS_DESIGN_AUTHORTITY]) 
SELECT   tags.Id, det.PROJECT_ID
into  #tempDesignAuthority
FROM [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS det
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS_DESIGN_AUTHORTITY design on det.Project_ID =  design.PROJECT_ID
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.LOCATION_MASTER loc on loc.LOCATION_ID =  design.DESIGN_AUTHORITY_ID
left join (select * from tags where TagCategoryId = 13) as Tags on tags.Name =  loc.location_description
where DET.UI_STATUS='Open' order by DET.PROJECT_ID 

--------TEMP----------- Product Line ([PRODUCT_LINE_MASTER]) 
SELECT   tags.Id, det.PROJECT_ID, det.PRODUCT_LINE, product.PRODUCT_LINE_description
into  #tempProductLine
FROM [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS det
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PRODUCT_LINE_MASTER product on det.PRODUCT_LINE =  product.PRODUCT_LINE_ID
left join (select * from tags where TagCategoryId = 9) as Tags on tags.Name =  product.PRODUCT_LINE_description
where DET.UI_STATUS='Open' order by DET.PROJECT_ID 



--step 4  Add projectDetails Data
 
 insert Into ProjectDetails (CreateDate , ModifiedByUser,[Version] ,LastModified , ProjectId , Salesforce, ProjectCategoryTagId , ProductLineTagId,
						ProjectClassificationTagId, ExportApplicationTypeTagId , DesignAuthorityTagId , ProjectProcessType,	
						ExportControlCode, EndUseDestinationCountry)
 
 select details.DATE_ENTERED, 'system' , 1, details.DATE_ENTERED, details.PROJECT_ID , details.CRM#, #tempProjCategory.Id, #tempProductLine.Id,
		#tempProjClassification.Id , 742 as ExportApplicationTypeTagId , #tempDesignAuthority.Id, workbook.VBPD_WORKBOOK_DESCRIPTION, '', ''

from [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS details
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.VBPD_WORKBOOK_MASTER workbook on workbook.[VBPD_WORKBOOK_ID] = details.VBPD_WORKBOOK_ID
inner join #tempProjClassification on #tempProjClassification.Project_ID = details.PROJECT_ID
inner join #tempProjCategory on #tempProjCategory.PROJECT_ID = details.PROJECT_ID
inner join #tempDesignAuthority on #tempDesignAuthority.PROJECT_ID =  details.PROJECT_ID
inner join #tempProductLine on #tempProductLine.PROJECT_ID = details.PROJECT_ID

where details.UI_STATUS='Open' order by details.PROJECT_ID
  --Drop Temp tables 
 Drop Table #tempProjClassification,#tempProjCategory , #tempDesignAuthority ,#tempProductLine



--STEP 5 Add Stages -----------------=========== NOTE : Fix CreateDate for all rows where stageNumber > 1

insert into stages (CreateDate ,modifiedByUser , ProjectId, IsComplete, StageNumber)
select  Case when gates.GATE_ID='Gate 1' then details.DATE_ENTERED
				else GETDATE() END, 'system' , gates.PROJECT_ID, 
	    Case when gates.GATE_ACTUAL_REVIEW_DATE is not null then 1 else 0 END , CONVERT(INT,REPLACE(gates.GATE_ID,'GATE ','')) StageNumber
	  
FROM [USSFA1SRPTPCH02].[ITTCo_PMO_PROD].[dbo].[PROJECT_GATE_HISTORY] gates
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS details on details.PROJECT_ID =gates.PROJECT_ID
	
where details.UI_STATUS='Open' and 
CONVERT(INT,REPLACE(details.GATE_ID,'GATE ','') ) >= CONVERT(INT,REPLACE(gates.GATE_ID,'GATE ','')) 
order by details.PROJECT_ID


--STEP 6 Add gates    -----------------=========== NOTE : Fix CreateDate - Not Sure about Decision Type 

insert Into Gates (CreateDate, ModifiedByUser,ProjectId,ActualReviewDate, Decision,Comments,StageConfigId)
Select GETDATE() , 'system' , details.PROJECT_ID ,  gates.Gate_Actual_Review_Date , 
case when Gates.Gate_STATUS = 'Go' then 2 
 when Gates.Gate_STATUS = 'On Hold' then 3 
 when  Gates.Gate_STATUS = 'Recycle' then 0
 END 
 , Gates.Gate_FINAL_COMMENT , CONVERT(INT,REPLACE(gates.GATE_ID,'GATE ',''))
FROM [USSFA1SRPTPCH02].[ITTCo_PMO_PROD].[dbo].[PROJECT_GATE_HISTORY] gates
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS details on details.PROJECT_ID =gates.PROJECT_ID
where details.UI_STATUS='Open' and gates.GATE_STATUS is not null 
order by details.PROJECT_ID 

-- STEP 7 Add project state history

insert Into ProjectStateHistories (CreateDate, ModifiedByUser,ProjectId, projectState)
Select Gates.GATE_ACTUAL_REVIEW_DATE  , 'system' , details.PROJECT_ID , 
case when Gates.Gate_STATUS = 'Go' then 1 
 when Gates.Gate_STATUS = 'On Hold' then 2 
 when  Gates.Gate_STATUS = 'Recycle' then 1
 END 
FROM [USSFA1SRPTPCH02].[ITTCo_PMO_PROD].[dbo].[PROJECT_GATE_HISTORY] gates
inner join [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS details on details.PROJECT_ID =gates.PROJECT_ID
where details.UI_STATUS='Open' and gates.GATE_STATUS is not null 
order by details.PROJECT_ID 

--Add projectState for projects which are only on gate1 (the new projects at gate1 has no state )
insert into [ProjectStateHistories] (CreateDate,ModifiedByUser,ProjectId,ProjectState)
SELECT  GETDATE(), 'system', d.ProjectId, 1
  FROM ProjectDetails d
  left join   [ProjectStateHistories] h  on d.ProjectId =h.ProjectId
  where h.id is null

--Step 8 Add projectTeam Mebmer - data from Step 1 

INSERT INTO Project_User (CreateDate,ModifiedByUser,ProjectId,UserId,JobDescriptionKey)
 Select dISTINCT t.CreateDate, 'system',Project_ID ,u.Id, SEGMENT_CAPTION  From  #temp1 t
 left join Users u on u.NetworkUsername =  t.USER_ID 

Drop table #temp1

print '!!!!!!USERS and PROJECT main data INSERTED!!!!!!'

-- ADD VBPD businessCase first 
insert into BusinessCases (
      [CreateDate]
      ,[ModifiedByUser]
      ,[Version]
      ,[LastModified]
      ,[StageId]
      ,[WillCustomerFundQual]
      ,[WillCustomerFundTooling]
      ,[ProbabiltyOfWin]
      ,[TargetFirstYearGrossMargin]
      ,[DiscountRate]
      ,[TaxRate]
      ,[LaborRate]
      ,[GpaRequirements]
      ,[ProjectScope]
      ,[WorkRequirementAmount]
      ,[StrategicAlignment]
      ,[AddToTecnicalAbilities]
      ,[ProjectCompletion]
      ,[TimeFromProjectCompletionToRevenueGeneration]
      ,[CustomerMarketAnalysis]
      ,[Changes]
      ,[FinancialStartYear])


SELECT 
 b.Gate_START_DATE,'system', 1 ,b.Gate_START_DATE AS LastModified,st.Id as StageId, 
	    case when  b.WILL_CUSTOMER_FUND_QUAL = 'No' then 0 else 1 End ,
		case when  b.WILL_CUSTOMER_FUND_TOOLING = 'No' then 0 else 1 End ,
		case when  b.PROBABILIY_OF_SUCCESS is null then 0 else b.PROBABILIY_OF_SUCCESS END  ,
		case when b.TARGET_1ST_YEAR_GROSS_MARGIN is null then 0 else b.TARGET_1ST_YEAR_GROSS_MARGIN END,
		 b.DISCOUNT_RATE, b.TAX_RATE , 100 as LaborRate , B.gpa_requirements, 
		'does not exist in old System' AS ProjectScope, 
		 -1 AS WorkReQuierementAmount,
		 0 AS StrategicAlignment ,
		 'does not exist in old System' as AddToTecnicalAbilities ,
		 '0001-01-01 00:00:00.0000000' AS ProjectCompletion ,
		 -1 AS TimeFromProjectCompletionToRevenueGeneration,
		 'does not exist in old System' as CustomerMarketAnalysis,
		 0 AS Changes,
		 YEAR(	b.Gate_START_DATE) AS DATA_STARTING_DAT

FROM USSFA1SRPTPCH02.ITTCo_PMO_PROD.dbo.PROJECT_GATE_FINANCIAL_DATA b
INNER JOIN Stages st ON st.ProjectId  = b.PROJECT_ID and   st.StageNumber in (2,3,4,5)
INNER JOIN [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS details ON details.PROJECT_ID =b.PROJECT_ID	
WHERE	details.UI_STATUS='Open' AND  b.Gate_Id IN ('Gate 2','Gate 3','Gate 4','Gate 5') 
		AND St.StageNumber = CONVERT(INT,REPLACE(b.GATE_ID,'GATE ',''))
		AND details.[VBPD_WORKBOOK_ID] =1
ORDER BY b.gate_id 

--Add vbpd Lite 
insert into BusinessCases (
      [CreateDate]
      ,[ModifiedByUser]
      ,[Version]
      ,[LastModified]
      ,[StageId]
      ,[WillCustomerFundQual]
      ,[WillCustomerFundTooling]
      ,[ProbabiltyOfWin]
      ,[TargetFirstYearGrossMargin]
      ,[DiscountRate]
      ,[TaxRate]
      ,[LaborRate]
      ,[GpaRequirements]
      ,[ProjectScope]
      ,[WorkRequirementAmount]
      ,[StrategicAlignment]
      ,[AddToTecnicalAbilities]
      ,[ProjectCompletion]
      ,[TimeFromProjectCompletionToRevenueGeneration]
      ,[CustomerMarketAnalysis]
      ,[Changes]
      ,[FinancialStartYear])


SELECT 
 b.Gate_START_DATE,'system', 1 ,b.Gate_START_DATE AS LastModified,st.Id as StageId, 
	    case when  b.WILL_CUSTOMER_FUND_QUAL = 'No' then 0 else 1 End ,
		case when  b.WILL_CUSTOMER_FUND_TOOLING = 'No' then 0 else 1 End ,
		case when  b.PROBABILIY_OF_SUCCESS is null then 0 else b.PROBABILIY_OF_SUCCESS END  ,
		case when b.TARGET_1ST_YEAR_GROSS_MARGIN is null then 0 else b.TARGET_1ST_YEAR_GROSS_MARGIN END,
		 b.DISCOUNT_RATE, b.TAX_RATE , 100 as LaborRate , B.gpa_requirements, 
		'does not exist in old System' AS ProjectScope, 
		 -1 AS WorkReQuierementAmount,
		 0 AS StrategicAlignment ,
		 'does not exist in old System' as AddToTecnicalAbilities ,
		 '0001-01-01 00:00:00.0000000' AS ProjectCompletion ,
		 -1 AS TimeFromProjectCompletionToRevenueGeneration,
		 'does not exist in old System' as CustomerMarketAnalysis,
		 0 AS Changes,
		 YEAR(	b.Gate_START_DATE) AS DATA_STARTING_DAT

FROM USSFA1SRPTPCH02.ITTCo_PMO_PROD.dbo.PROJECT_GATE_FINANCIAL_DATA b
INNER JOIN Stages st ON st.ProjectId  = b.PROJECT_ID
INNER JOIN [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS details ON details.PROJECT_ID =b.PROJECT_ID	
WHERE	details.UI_STATUS='Open' 
		AND St.StageNumber = CONVERT(INT,REPLACE(b.GATE_ID,'GATE ',''))
		AND details.[VBPD_WORKBOOK_ID] =2
ORDER BY b.gate_id 

--Step 8 Add Financial Data


--Add VBPD data 

insert into FinancialData ([CreateDate]
      ,[ModifiedByUser]
      ,[BusinessCaseId]
      ,[Year]
      ,[Quantity]
      ,[StdCostEstimated]
      ,[SalesPriceEstimated]
      ,[GPACapital]
      ,[GPAExpense]
      ,[QualCosts]
      ,[OtherDevelopmentExpenses])
Select bu.CreateDate ,'system', bu.Id ,table1.[YEAR] ,
		table1.Quantity   , 
		table1.AUC,
		table1.SalesPrice ,
		table1.GPACapital ,
		0, 
		table1.QualCost , 
		table1.OtherDevEXP
from (
SELECT  PIV.PROJECT_ID, PIV.GATE_ID, [YEAR],  [3] as GPACapital , [5] as SalesPrice, 
									[7] as QualCost, [9] as OtherDevEXP ,[18] as Quantity, [19] as AUC

FROM 
(SELECT PROJECT_ID, GATE_ID, REV_PARAM_ID, [YEAR], REV_PARAM_VALUE
         FROM USSFA1SRPTPCH02.ITTCo_PMO_PROD.[bobj].[vwPROJECT_GATE_FINANACIAL_SUMMARY_ALL]) as PVT
PIVOT 
     (Sum([REV_PARAM_VALUE]) for REV_PARAM_ID IN ([3],[5],[7],[9] ,[18],[19])) as PIV
) as table1 
inner join USSFA1SRPTPCH02.ITTCo_PMO_PROD.dbo.PRoject_details d on d.Project_ID = table1.Project_ID
inner join stages s on  s.ProjectId= d.project_id and s.StageNumber = CONVERT(INT,REPLACE(table1.GATE_ID,'GATE ',''))
inner join BusinessCases bu on bu.StageId = s.id and bu.Version=1
WHERE d.UI_STATUS='Open' and  d.[VBPD_WORKBOOK_ID] =1 AND table1.Gate_ID in ('Gate 2','Gate 3','Gate 4','Gate 5')
and table1.[YEAR] <>0

ORDER BY STAGEID , table1.GATE_ID, table1.[YEAR]

--Add - VBPD LITE DATA 

insert into FinancialData ([CreateDate]
      ,[ModifiedByUser]
      ,[BusinessCaseId]
      ,[Year]
      ,[Quantity]
      ,[StdCostEstimated]
      ,[SalesPriceEstimated]
      ,[GPACapital]
      ,[GPAExpense]
      ,[QualCosts]
      ,[OtherDevelopmentExpenses])
Select bu.CreateDate ,'system', bu.Id ,table1.[YEAR] ,
  table1.Quantity   , 
table1.AUC  ,
 table1.SalesPrice 
		, table1.GPACapital , 0, table1.QualCost , table1.OtherDevEXP
from (
SELECT  PIV.PROJECT_ID, PIV.GATE_ID, [YEAR],  [3] as GPACapital , [5] as SalesPrice, 
									[7] as QualCost, [9] as OtherDevEXP ,[18] as Quantity, [19] as AUC
FROM 
(SELECT PROJECT_ID, GATE_ID, REV_PARAM_ID, [YEAR], REV_PARAM_VALUE
         FROM USSFA1SRPTPCH02.ITTCo_PMO_PROD.[bobj].[vwPROJECT_GATE_FINANACIAL_SUMMARY_ALL]) as PVT
PIVOT 
     (Sum([REV_PARAM_VALUE]) for REV_PARAM_ID IN ([3],[5],[7],[9] ,[18],[19])) as PIV
) as table1 
inner join USSFA1SRPTPCH02.ITTCo_PMO_PROD.dbo.PRoject_details d on d.Project_ID = table1.Project_ID
inner join stages s on  s.ProjectId= d.project_id
and s.StageNumber = CONVERT(INT,
case when REPLACE(table1.GATE_ID,'GATE ','') = 'A' then 1
when REPLACE(table1.GATE_ID,'GATE ','') = 'B' then 2 End)
inner join BusinessCases bu on bu.StageId = s.id 
WHERE d.UI_STATUS='Open' and  d.[VBPD_WORKBOOK_ID] =2 and table1.[YEAR] <>0 


--Step 9 Add stage 5 for vbpd as version 2 on stage 4 , check vbpd lite gate C if exist 

--VBPD finance for OldGate5
/****** Script for SelectTopNRows command from SSMS  ******/



------Step 10 ADD RISKs AND MITIGATIONs

Insert into Risks(CreateDate,ModifiedByUser,Name,RiskPropability,RiskTypeTagId,RiskImpactTagId,StageId)
SELECT  GETDATE(), 'system' , risk.RISK_TITLE ,
		tags.id as TagOCCURRENCE,
		tags1.id as TagRiskTYPE,
		tags2.id as TagRISKIMPACT, 
		st.id 
FROM USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[PROJECT_GATE_RISK_DETAILS] risk
left  join  USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_OCCURRENCE_MASTER  occ on occ.RISK_OCCURRENCE_ID =  risk.PROBABILITY_OCCURRENCE
left Join  USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_TYPE_MASTER rtype on rtype.RISK_TYPE_ID = risk.RISK_TYPE
left join   USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_IMPACT_MASTER impact on impact.RISK_IMPACT_ID = risk.POTENTIAL_IMPACT
left join  [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS details on risk.PROJECT_ID = details.PROJECT_ID
inner join Tags tags on tags.Name = occ.RISK_OCCURRENCE_DESC AND tags.TagCategoryId=11
inner join Tags tags1 on tags1.Name = rtype.RISK_TYPE_DESC AND tags1.TagCategoryId=10
inner join Tags tags2 on tags2.Name = impact.RISK_IMPACT_DESC AND tags2.TagCategoryId=12
INNER join Stages st on st.ProjectId = details.PROJECT_ID and
  st.StageNumber = 
  (case 
  when CONVERT(INT,REPLACE(risk.GATE_ID,'GATE ','')) =1 then 3 
  when CONVERT(INT,REPLACE(risk.GATE_ID,'GATE ','')) =2 then 3
  Else CONVERT(INT,REPLACE(risk.GATE_ID,'GATE ',''))
  END)
where details.UI_STATUS='Open'and risk.RISK_TITLE is not null and risk.RISK_TITLE<>'' 
and details.[VBPD_WORKBOOK_ID] =1 
order by details.PROJECT_ID 


Insert into Risks(CreateDate,ModifiedByUser,Name,RiskPropability,RiskTypeTagId,RiskImpactTagId,StageId)
SELECT  GETDATE(), 'system' , risk.RISK_TITLE ,
		tags.id as TagOCCURRENCE,
		tags1.id as TagRiskTYPE,
		tags2.id as TagRISKIMPACT, 
		st.id 
FROM USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[PROJECT_GATE_RISK_DETAILS] risk
left  join  USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_OCCURRENCE_MASTER  occ on occ.RISK_OCCURRENCE_ID =  risk.PROBABILITY_OCCURRENCE
left Join  USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_TYPE_MASTER rtype on rtype.RISK_TYPE_ID = risk.RISK_TYPE
left join   USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_IMPACT_MASTER impact on impact.RISK_IMPACT_ID = risk.POTENTIAL_IMPACT
left join  [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS details on risk.PROJECT_ID = details.PROJECT_ID
inner join Tags tags on tags.Name = occ.RISK_OCCURRENCE_DESC AND tags.TagCategoryId=11
inner join Tags tags1 on tags1.Name = rtype.RISK_TYPE_DESC AND tags1.TagCategoryId=10
inner join Tags tags2 on tags2.Name = impact.RISK_IMPACT_DESC AND tags2.TagCategoryId=12
INNER join Stages st on st.ProjectId = details.PROJECT_ID and
  st.StageNumber = 
  (case 
  when CONVERT(INT,REPLACE(risk.GATE_ID,'GATE ','')) =1 then 2 
  Else CONVERT(INT,REPLACE(risk.GATE_ID,'GATE ',''))
  END)
where details.UI_STATUS='Open'and risk.RISK_TITLE is not null and risk.RISK_TITLE<>'' 
and details.[VBPD_WORKBOOK_ID] =2 
order by details.PROJECT_ID 
 
 --===MITIGATIONS 


insert into Mitigations (CreateDate,ModifiedByUser,MitigationPlan, ResponsibilityUserId,RiskId ,TargetDate)
SELECT GetDate() , 'system',Mitigation.RISK_MITIGATION_PLAN ,  u.Id,
				r.id , case when mitigation.MITIGATION_TARGET_DATE is null then cast(-0 as datetime) else  mitigation.MITIGATION_TARGET_DATE END 
FROM USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[PROJECT_GATE_RISK_DETAILS] risk

left  join  USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_OCCURRENCE_MASTER  occ on occ.RISK_OCCURRENCE_ID =  risk.PROBABILITY_OCCURRENCE
left Join  USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_TYPE_MASTER rtype on rtype.RISK_TYPE_ID = risk.RISK_TYPE
left join   USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_IMPACT_MASTER impact on impact.RISK_IMPACT_ID = risk.POTENTIAL_IMPACT
left join  [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS details on risk.PROJECT_ID = details.PROJECT_ID
inner join Tags tags on tags.Name = occ.RISK_OCCURRENCE_DESC AND tags.TagCategoryId=11
inner join Tags tags1 on tags1.Name = rtype.RISK_TYPE_DESC AND tags1.TagCategoryId=10
inner join Tags tags2 on tags2.Name = impact.RISK_IMPACT_DESC AND tags2.TagCategoryId=12
INNER join Stages st on st.ProjectId = details.PROJECT_ID and
  st.StageNumber = 
  (case 
  when CONVERT(INT,REPLACE(risk.GATE_ID,'GATE ','')) =1 then 3 
  when CONVERT(INT,REPLACE(risk.GATE_ID,'GATE ','')) =2 then 3
  Else CONVERT(INT,REPLACE(risk.GATE_ID,'GATE ',''))
  END)
inner join Risks r on risk.RISK_TITLE = r.Name
left join USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].PROJECT_GATE_RISK_MITIGATION_DETAILS mitigation on mitigation.PROJ_GATE_RISK_ID = risk.PROJ_GATE_RISK_ID
inner join Users u on u.NetworkUsername = mitigation.MITIGATION_RESPONSIBLE
where details.UI_STATUS='Open'and risk.RISK_TITLE is not null and risk.RISK_TITLE<>'' 
and details.[VBPD_WORKBOOK_ID] =1  and mitigation.RISK_MITIGATION_PLAN is not null
order by details.PROJECT_ID 

--LITE 

insert into Mitigations (CreateDate,ModifiedByUser,MitigationPlan, ResponsibilityUserId,RiskId ,TargetDate)

SELECT GetDate() , 'system',Mitigation.RISK_MITIGATION_PLAN , u.Id,
				r.id , case when mitigation.MITIGATION_TARGET_DATE is null then cast(-0 as datetime) else  mitigation.MITIGATION_TARGET_DATE END 
FROM USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[PROJECT_GATE_RISK_DETAILS] risk
left  join  USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_OCCURRENCE_MASTER  occ on occ.RISK_OCCURRENCE_ID =  risk.PROBABILITY_OCCURRENCE
left Join  USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_TYPE_MASTER rtype on rtype.RISK_TYPE_ID = risk.RISK_TYPE
left join   USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].RISK_IMPACT_MASTER impact on impact.RISK_IMPACT_ID = risk.POTENTIAL_IMPACT
left join  [USSFA1SRPTPCH02].ITTCo_PMO_PROD.dbo.PROJECT_DETAILS details on risk.PROJECT_ID = details.PROJECT_ID
inner join Tags tags on tags.Name = occ.RISK_OCCURRENCE_DESC AND tags.TagCategoryId=11
inner join Tags tags1 on tags1.Name = rtype.RISK_TYPE_DESC AND tags1.TagCategoryId=10
inner join Tags tags2 on tags2.Name = impact.RISK_IMPACT_DESC AND tags2.TagCategoryId=12
INNER join Stages st on st.ProjectId = details.PROJECT_ID and
  st.StageNumber = 
  (case 
  when CONVERT(INT,REPLACE(risk.GATE_ID,'GATE ','')) =1 then 2 
  Else CONVERT(INT,REPLACE(risk.GATE_ID,'GATE ',''))
  END)
inner join Risks r on risk.RISK_TITLE = r.Name
left join USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].PROJECT_GATE_RISK_MITIGATION_DETAILS mitigation on mitigation.PROJ_GATE_RISK_ID = risk.PROJ_GATE_RISK_ID
inner join Users u on u.NetworkUsername = mitigation.MITIGATION_RESPONSIBLE
where details.UI_STATUS='Open'and risk.RISK_TITLE is not null and risk.RISK_TITLE<>'' 
and details.[VBPD_WORKBOOK_ID] =2 and mitigation.PROJ_GATE_RISK_ID is not null 
order by details.PROJECT_ID 

 -- Step 11  ADD CUstomer NOTE!!!!!!!!!!! Add all customers from [CUSTOMER_MASTER] to Tags 

 --Add missing Customer 
  Insert into Tags (CreateDate,ModifiedByUser ,Name , TagCategoryId)
  Select GETDATE(), 'system', customer.Customer_Name , 8
  FROM USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[PROJECT_DETAILS_CUSTOMERS] c
  inner join   USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[PROJECT_DETAILS] details on details.[PROJECT_ID] = c.[PROJECT_ID]
  left join USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[CUSTOMER_MASTER] customer on customer.CUSTOMER_ID = c.CUSTOMER_ID
  left join [Tags] t on t.name =  customer.CUSTOMER_NAME and  TagCategoryId= 8
  inner join Projects p on p.Id = details.PROJECT_ID
  inner join projectDetails d on d.ProjectId = p.Id
  where details.UI_STATUS='Open' and t.Id is null


  Insert into ProjectDetail_Customers (CreateDate , CustomersTagId ,ModifiedByUser , ProjectDetailId ) 
  Select Distinct GETDATE() , t.Id, 'system', d.Id
  FROM USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[PROJECT_DETAILS_CUSTOMERS] c
  inner join   USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[PROJECT_DETAILS] details on details.[PROJECT_ID] = c.[PROJECT_ID]
  left join USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[CUSTOMER_MASTER] customer on customer.CUSTOMER_ID = c.CUSTOMER_ID
  left join [Tags] t on t.name =  customer.CUSTOMER_NAME and  TagCategoryId= 8
  inner join Projects p on p.Id = details.PROJECT_ID
  inner join projectDetails d on d.ProjectId = p.Id
  where details.UI_STATUS='Open'

-- Step 12 Add sales Regions per ProjectDetailId 

insert into ProjectDetail_SalesRegions (CreateDate,ModifiedByUser ,ProjectDetailId ,SalesRegionTagId)
Select GETDATE(), 'system', details.	Id  , t.Id from USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].PROJECT_DETAILS_SALES_REGION region 
inner join Projects p on p.Id = region.PROJECT_ID
inner join ProjectDetails details on details.ProjectId = p.Id
inner join  USSFA1SRPTPCH02.[ITTCo_PMO_PROD].[dbo].[REGION_MASTER] m on m .REGION_ID = region.SALES_REGION_ID
left join Tags t on t.Name  =  m.REGION_DESCRIPTION and t.TagCategoryId= 3

--Step 13 Add GateKeepers  -MISSINGGGGG






--====================================================================================================
--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
--====================================================================================================
--Users Fixes
UPDATE Users SET DisplayName = 'Kalyva, Georgia' WHERE NetworkUsername = 'global\georgia.kalyva'
UPDATE Users SET DisplayName = 'Dellis, Efthimios' WHERE NetworkUsername = 'global\efthimios.dellis'
UPDATE Users SET DisplayName = 'Hadjimylo, Christoph' WHERE NetworkUsername = 'global\christoph.hadjimylo'
UPDATE Users SET DisplayName = 'Bogri, Georgia' WHERE NetworkUsername = 'global\georgia.bogri'
UPDATE Users SET DisplayName = 'Marolachakis, Konstantinos' WHERE NetworkUsername = 'global\konstantinos.marolac'
UPDATE Users SET DisplayName = 'Giannakopoulos, Ioannis' WHERE NetworkUsername = 'global\ioannis.giannakop'
UPDATE Users SET DisplayName = 'Zaragkidis, Christos' WHERE NetworkUsername = 'global\christos.zaragkidis'
UPDATE Users SET DisplayName = 'Karagiannakis, George' WHERE NetworkUsername = 'global\george.karagiannakis'
UPDATE Users SET DisplayName = 'Lopez, Adela - Connect & Control Technologies' WHERE NetworkUsername = 'global\adela.lopez'
UPDATE Users SET DisplayName = 'Schaller, Alain - Connect & Control Technologies' WHERE NetworkUsername = 'global\alain.schaller'
UPDATE Users SET DisplayName = 'Albrecht, Friedrich - Connect & Control Technologies' WHERE NetworkUsername = 'global\albrechtf'
UPDATE Users SET DisplayName = 'Espinosa, Alejandro - Connect & Control Technologies' WHERE NetworkUsername = 'global\alejandro.espinosa'
UPDATE Users SET DisplayName = 'Zimmermann, Andreas - Connect & Control Technologies' WHERE NetworkUsername = 'global\andreas.zimmermann'
UPDATE Users SET DisplayName = 'Ablott, Andrew - Connect & Control Technologies' WHERE NetworkUsername = 'global\andrew.ablott'
UPDATE Users SET DisplayName = 'Desai, Anil - Connect & Control Technologies' WHERE NetworkUsername = 'global\anilkumar.desai'
UPDATE Users SET DisplayName = 'Vittimberga, Anthony' WHERE NetworkUsername = 'global\anthony.vittimberga'
UPDATE Users SET DisplayName = 'Renolayan, Arnel - Connect & Control Technologies' WHERE NetworkUsername = 'global\arnel.renolayan'
UPDATE Users SET DisplayName = 'Dunn, Art - Connect & Control Technologies' WHERE NetworkUsername = 'global\art.dunn'
UPDATE Users SET DisplayName = 'Wu, Betty - Connect & Control Technologies' WHERE NetworkUsername = 'global\betty.wu'
UPDATE Users SET DisplayName = 'Bihrer, Markus - Connect & Control Technologies' WHERE NetworkUsername = 'global\bihrerm'
UPDATE Users SET DisplayName = 'Zawicki, Bill - Connect & Control Technologies' WHERE NetworkUsername = 'global\bill.zawicki'
UPDATE Users SET DisplayName = 'Wan, Billy - Connect & Control Technologies' WHERE NetworkUsername = 'global\billy.wan'
UPDATE Users SET DisplayName = 'Romfo, Bret' WHERE NetworkUsername = 'global\bret.romfo'
UPDATE Users SET DisplayName = 'Su, Camy - Connect & Control Technologies' WHERE NetworkUsername = 'global\camy.su'
UPDATE Users SET DisplayName = 'Go, Carolyn - Connect & Control Technologies' WHERE NetworkUsername = 'global\carolyn.go'
UPDATE Users SET DisplayName = 'Amador, Cesar - Connect & Control Technologies' WHERE NetworkUsername = 'global\cesar.amador'
UPDATE Users SET DisplayName = 'Gonzalez, Julio - Connect & Control Technologies' WHERE NetworkUsername = 'global\cesar.gonzalez'
UPDATE Users SET DisplayName = 'Chai, Chris - Connect & Control Technologies' WHERE NetworkUsername = 'global\chris.chai'
UPDATE Users SET DisplayName = 'Citarelli, Gianni - Connect & Control Technologies' WHERE NetworkUsername = 'global\citarellig'
UPDATE Users SET DisplayName = 'Clerici, Roberto - Connect & Control Technologies' WHERE NetworkUsername = 'global\clericir'
UPDATE Users SET DisplayName = 'Crivelli, Franco Renato - Connect & Control Technologies' WHERE NetworkUsername = 'global\crivellif'
UPDATE Users SET DisplayName = 'Barnett, Dan - Connect & Control Technologies' WHERE NetworkUsername = 'global\dan.barnett'
UPDATE Users SET DisplayName = 'Koo, Daniel - Connect & Control Technologies' WHERE NetworkUsername = 'global\daniel.koo'
UPDATE Users SET DisplayName = 'Shad, Davar - Connect & Control Technologies' WHERE NetworkUsername = 'global\davar.shad'
UPDATE Users SET DisplayName = 'Zhou, Dave - Connect & Control Technologies' WHERE NetworkUsername = 'global\dave.zhou'
UPDATE Users SET DisplayName = 'Campa, David' WHERE NetworkUsername = 'global\david.campa'
UPDATE Users SET DisplayName = 'Crenshaw, David - Connect & Control Technologies' WHERE NetworkUsername = 'global\david.crenshaw'
UPDATE Users SET DisplayName = 'Gonzalez, David - Connect & Control Technologies' WHERE NetworkUsername = 'global\david.gonzalez'
UPDATE Users SET DisplayName = 'Lecce, Dom - Connect & Control Technologies' WHERE NetworkUsername = 'global\dom.lecce'
UPDATE Users SET DisplayName = 'Durigon, Alessandro - Connect & Control Technologies' WHERE NetworkUsername = 'global\durigona'
UPDATE Users SET DisplayName = 'Barajas, Victor - Connect & Control Technologies' WHERE NetworkUsername = 'global\eduardo.barajas'
UPDATE Users SET DisplayName = 'Wong, Edward - Connect & Control Technologies' WHERE NetworkUsername = 'global\edward.wong'
UPDATE Users SET DisplayName = 'Alawneh, Ehab' WHERE NetworkUsername = 'global\ehab.alawneh'
UPDATE Users SET DisplayName = 'Vasquez, Everardo - Connect & Control Technologies' WHERE NetworkUsername = 'global\evasquez'
UPDATE Users SET DisplayName = 'Frenzel, Andreas - Connect & Control Technologies' WHERE NetworkUsername = 'global\frenzela'
UPDATE Users SET DisplayName = 'Blazas, George - Connect & Control Technologies' WHERE NetworkUsername = 'global\george.blazas'
UPDATE Users SET DisplayName = 'Hagmann, Bernd - Connect & Control Technologies' WHERE NetworkUsername = 'global\hagmannb'
UPDATE Users SET DisplayName = 'Tran, Henry - Connect & Control Technologies' WHERE NetworkUsername = 'global\henry.tran'
UPDATE Users SET DisplayName = 'Liu, Herry - Connect & Control Technologies' WHERE NetworkUsername = 'global\herry.liu'
UPDATE Users SET DisplayName = 'Nguyen, Hong' WHERE NetworkUsername = 'global\hong.nguyen'
UPDATE Users SET DisplayName = 'Honner, Alfred - Connect & Control Technologies' WHERE NetworkUsername = 'global\honneral'
UPDATE Users SET DisplayName = 'Qiao, Huawei - Connect & Control Technologies' WHERE NetworkUsername = 'global\huawei.qiao'
UPDATE Users SET DisplayName = 'Ikenaka, Kazuo - Connect & Control Technologies' WHERE NetworkUsername = 'global\ikenakak'
UPDATE Users SET DisplayName = 'Izzo, Stefano - Connect & Control Technologies' WHERE NetworkUsername = 'global\izzos'
UPDATE Users SET DisplayName = 'Cai, Jason - Connect & Control Technologies' WHERE NetworkUsername = 'global\jason.cai'
UPDATE Users SET DisplayName = 'Putman, Jason - Connect & Control Technologies' WHERE NetworkUsername = 'global\jason.putman'
UPDATE Users SET DisplayName = 'Eng, Jeff - Connect & Control Technologies' WHERE NetworkUsername = 'global\jeff.eng'
UPDATE Users SET DisplayName = 'Kang, Jerry - Connect & Control Technologies' WHERE NetworkUsername = 'global\jerry.kang'
UPDATE Users SET DisplayName = 'Xie, Jerry - Connect & Control Technologies' WHERE NetworkUsername = 'global\jerry.xie'
UPDATE Users SET DisplayName = 'Willhite, Jim - Connect & Control Technologies' WHERE NetworkUsername = 'global\jim.willhite'
UPDATE Users SET DisplayName = 'Jordan, Peter - Connect & Control Technologies' WHERE NetworkUsername = 'global\jordanp'
UPDATE Users SET DisplayName = 'Madrid, Jorge - Connect & Control Technologies' WHERE NetworkUsername = 'global\jorge.madrid'
UPDATE Users SET DisplayName = 'Pazos, Jose - Connect & Control Technologies' WHERE NetworkUsername = 'global\jose.pazos'
UPDATE Users SET DisplayName = 'Curiel, Juan - Connect & Control Technologies' WHERE NetworkUsername = 'global\juan.curiel'
UPDATE Users SET DisplayName = 'Muñoz, Juan - Connect & Control Technologies' WHERE NetworkUsername = 'global\juan.munoz'
UPDATE Users SET DisplayName = 'Kabott, Ralf - Connect & Control Technologies' WHERE NetworkUsername = 'global\kabottr'
UPDATE Users SET DisplayName = 'Reeves, Kevin - Connect & Control Technologies' WHERE NetworkUsername = 'global\kevin.reeves'
UPDATE Users SET DisplayName = 'Wilson, Kirk - Connect & Control Technologies' WHERE NetworkUsername = 'global\kirk.wilson'
UPDATE Users SET DisplayName = 'Koenig, Hans-Martin - Connect & Control Technologies' WHERE NetworkUsername = 'global\koenighm'
UPDATE Users SET DisplayName = 'Krueger, Franziska - Connect & Control Technologies' WHERE NetworkUsername = 'global\kruegerf'
UPDATE Users SET DisplayName = 'Schramm, Larissa - Connect & Control Technologies' WHERE NetworkUsername = 'global\larissa.schramm'
UPDATE Users SET DisplayName = 'Ensign, Larry - Connect & Control Technologies' WHERE NetworkUsername = 'global\larry.ensign'
UPDATE Users SET DisplayName = 'Nguyen, Le - Connect & Control Technologies' WHERE NetworkUsername = 'global\le.nguyen'
UPDATE Users SET DisplayName = 'Wang, Lik - Connect & Control Technologies' WHERE NetworkUsername = 'global\lik.wang'
UPDATE Users SET DisplayName = 'Maier-Kaps, Andrea - Connect & Control Technologies' WHERE NetworkUsername = 'global\maiera'
UPDATE Users SET DisplayName = 'Maier, Thomas - Connect & Control Technologies' WHERE NetworkUsername = 'global\maiert'
UPDATE Users SET DisplayName = 'Goedecke, Marcel - Connect & Control Technologies' WHERE NetworkUsername = 'global\marcel.goedecke'
UPDATE Users SET DisplayName = 'Marchesi, Mauro - Connect & Control Technologies' WHERE NetworkUsername = 'global\marchesim'
UPDATE Users SET DisplayName = 'Cota, Marco - Connect & Control Technologies' WHERE NetworkUsername = 'global\marco.cota'
UPDATE Users SET DisplayName = 'Archontidou, Maria - Connect & Control Technologies' WHERE NetworkUsername = 'global\maria.archontidou'
UPDATE Users SET DisplayName = 'Meza, Maria - Connect & Control Technologies' WHERE NetworkUsername = 'global\maria.meza'
UPDATE Users SET DisplayName = 'Gonzalez, Mario - Connect & Control Technologies' WHERE NetworkUsername = 'global\mario.gonzalez'
UPDATE Users SET DisplayName = 'Webb, Matt' WHERE NetworkUsername = 'global\matthew.webb'
UPDATE Users SET DisplayName = 'Mayer, Stefan - Connect & Control Technologies' WHERE NetworkUsername = 'global\mayers'
UPDATE Users SET DisplayName = 'Gu, Mike - Connect & Control Technologies' WHERE NetworkUsername = 'global\mike.gu'
UPDATE Users SET DisplayName = 'Regola, Mike - Connect & Control Technologies' WHERE NetworkUsername = 'global\mike.regola'
UPDATE Users SET DisplayName = 'Walsh, Mike - Connect & Control Technologies' WHERE NetworkUsername = 'global\mike.walsh'
UPDATE Users SET DisplayName = 'Fidaali, Murtaza - Connect & Control Technologies' WHERE NetworkUsername = 'global\murtaza.fidaali'
UPDATE Users SET DisplayName = 'Heckenberger, Nadja - Connect & Control Technologies' WHERE NetworkUsername = 'global\nadja.heckenberger'
UPDATE Users SET DisplayName = 'Mahaffy, Neil - Connect & Control Technologies' WHERE NetworkUsername = 'global\neil.mahaffy'
UPDATE Users SET DisplayName = 'Kreger, Nicki - Connect & Control Technologies' WHERE NetworkUsername = 'global\nicki.kreger'
UPDATE Users SET DisplayName = 'Miller, Olivier - Connect & Control Technologies' WHERE NetworkUsername = 'global\omiller'
UPDATE Users SET DisplayName = 'Panholzer, Jochen - Connect & Control Technologies' WHERE NetworkUsername = 'global\panholzerj'
UPDATE Users SET DisplayName = 'Perez, Patricia - Connect & Control Technologies' WHERE NetworkUsername = 'global\patricia.perez'
UPDATE Users SET DisplayName = 'Alpers, Paul - Connect & Control Technologies' WHERE NetworkUsername = 'global\paul.alpers'
UPDATE Users SET DisplayName = 'Pellegatta, Marco - Connect & Control Technologies' WHERE NetworkUsername = 'global\pellegattam'
UPDATE Users SET DisplayName = 'Pijpers, Richard - Connect & Control Technologies' WHERE NetworkUsername = 'global\pijpersri'
UPDATE Users SET DisplayName = 'Raad, Achim - Connect & Control Technologies' WHERE NetworkUsername = 'global\raada'
UPDATE Users SET DisplayName = 'Glocker, Ralf - Connect & Control Technologies' WHERE NetworkUsername = 'global\ralf.glocker'
UPDATE Users SET DisplayName = 'Perez, Rene - Connect & Control Technologies' WHERE NetworkUsername = 'global\rene.perez'
UPDATE Users SET DisplayName = 'Thomas, Richard - Connect & Control Technologies' WHERE NetworkUsername = 'global\richard.thomas'
UPDATE Users SET DisplayName = 'Catoe, Rick - Connect & Control Technologies' WHERE NetworkUsername = 'global\rick.catoe'
UPDATE Users SET DisplayName = 'Bonacina, Roberto - Connect & Control Technologies' WHERE NetworkUsername = 'global\roberto.bonacina'
UPDATE Users SET DisplayName = 'Zingaro, Roberto - ITT Interconnect Solutions' WHERE NetworkUsername = 'global\roberto.zingaro'
UPDATE Users SET DisplayName = 'He, Robin - Connect & Control Technologies' WHERE NetworkUsername = 'global\robin.he'
UPDATE Users SET DisplayName = 'Rossi, Massimo - Connect & Control Technologies' WHERE NetworkUsername = 'global\rossim'
UPDATE Users SET DisplayName = 'Villarruel, Sal - Connect & Control Technologies' WHERE NetworkUsername = 'global\sal.villarruel'
UPDATE Users SET DisplayName = 'Chow, Sam - Connect & Control Technologies' WHERE NetworkUsername = 'global\sam.chow'
UPDATE Users SET DisplayName = 'Schremmer, Andreas - Connect & Control Technologies' WHERE NetworkUsername = 'global\schremmera'
UPDATE Users SET DisplayName = 'Cohen, Scott' WHERE NetworkUsername = 'global\scott.cohen'
UPDATE Users SET DisplayName = 'Richter, Sebastian - Connect & Control Technologies' WHERE NetworkUsername = 'global\sebastian.richter'
UPDATE Users SET DisplayName = 'Swenson, Shawn' WHERE NetworkUsername = 'global\shawn.swenson'
UPDATE Users SET DisplayName = 'Sigle, Egon - Connect & Control Technologies' WHERE NetworkUsername = 'global\siglee'
UPDATE Users SET DisplayName = 'Gonzalez, Silvia - Connect & Control Technologies' WHERE NetworkUsername = 'global\silvia.gonzalez'
UPDATE Users SET DisplayName = 'Singer, Helmut - Connect & Control Technologies' WHERE NetworkUsername = 'global\singerh'
UPDATE Users SET DisplayName = 'Nagel, Stefan - Connect & Control Technologies' WHERE NetworkUsername = 'global\stefan.nagel'
UPDATE Users SET DisplayName = 'Nguyen, Steve - Connect & Control Technologies' WHERE NetworkUsername = 'global\steve.nguyen'
UPDATE Users SET DisplayName = 'Shi, Stone - Connect & Control Technologies' WHERE NetworkUsername = 'global\stone.shi'
UPDATE Users SET DisplayName = 'Tang, Thomas - Connect & Control Technologies' WHERE NetworkUsername = 'global\thomas.tang'
UPDATE Users SET DisplayName = 'Thume, Ruediger - Connect & Control Technologies' WHERE NetworkUsername = 'global\thumer'
UPDATE Users SET DisplayName = 'White, Tom - Connect & Control Technologies' WHERE NetworkUsername = 'global\tom.white'
UPDATE Users SET DisplayName = 'Hu, Tommy' WHERE NetworkUsername = 'global\tommy.hu'
UPDATE Users SET DisplayName = 'En, Traluch' WHERE NetworkUsername = 'global\traluch.en'
UPDATE Users SET DisplayName = 'Pham, Trung' WHERE NetworkUsername = 'global\trung.pham'
UPDATE Users SET DisplayName = 'Shah, Vijay - ITT Interconnect Solutions' WHERE NetworkUsername = 'global\vijay.shah'
UPDATE Users SET DisplayName = 'Chan, Vincent - Connect & Control Technologies' WHERE NetworkUsername = 'global\vincent.chan'
UPDATE Users SET DisplayName = 'Jensen, Wade - Connect & Control Technologies' WHERE NetworkUsername = 'global\wade.jensen'
UPDATE Users SET DisplayName = 'King, Wayde - Connect & Control Technologies' WHERE NetworkUsername = 'global\wayde.king'
UPDATE Users SET DisplayName = 'Lau, Billy - Connect & Control Technologies' WHERE NetworkUsername = 'global\weizhong.liu'
UPDATE Users SET DisplayName = 'Werder, Gerhard - Connect & Control Technologies' WHERE NetworkUsername = 'global\werderg'
UPDATE Users SET DisplayName = 'Werner, Oliver - Connect & Control Technologies' WHERE NetworkUsername = 'global\wernero'
UPDATE Users SET DisplayName = 'Morgan, Wes - Connect & Control Technologies' WHERE NetworkUsername = 'global\wes.morgan'
UPDATE Users SET DisplayName = 'Luo, Ye - Connect & Control Technologies' WHERE NetworkUsername = 'global\ye.luo'
UPDATE Users SET DisplayName = 'Wang, Zheyuan - Connect & Control Technologies' WHERE NetworkUsername = 'global\zheyuan.wang'
UPDATE Users SET DisplayName = 'Zeng, Zy - Connect & Control Technologies' WHERE NetworkUsername = 'global\zy.zeng'



