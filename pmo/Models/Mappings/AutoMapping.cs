using AutoMapper;
using dbModels;
using dbModels.Report;
using dto;
using forms;

namespace pmo.Models.Mappings {
    public class AutoMapping : Profile {
        public AutoMapping() {
            CreateMap<User, UserForm>().ReverseMap().ReverseMap();
            CreateMap<StageConfig, StageConfigForm>().ReverseMap();
            CreateMap<Role, RoleForm>().ReverseMap();
            CreateMap<Tag, TagForm>().ReverseMap();
            CreateMap<TagCategory, TagCategoryForm>().ReverseMap();
            CreateMap<Project, VBPDForm>().ReverseMap();
            CreateMap<ProjectDetail, VBPDForm>().ReverseMap();
            CreateMap<ProjectDetail, VBPDProjectDetailForm>().ReverseMap();
            CreateMap<CustomerDesignApproval, CustomerDesignApprovalForm>().ReverseMap();
            CreateMap<Schedule, ScheduleForm>().ReverseMap();
            CreateMap<InvestmentPlan, InvestmentPlanForm>().ReverseMap();
            CreateMap<KeyCharacteristic, KeyCharacteristicForm>().ReverseMap();
            CreateMap<ProductIntroChecklist, ProductIntroChecklistForm>().ReverseMap();
            CreateMap<Risk, RiskForm>().ReverseMap();
            CreateMap<Gate, NavigationGate>().ReverseMap();
            CreateMap<Stage, NavigationStage>().ReverseMap();
            CreateMap<ProductInfrigmentPatentability, ProductInfrigmentPatentabilityForm>().ReverseMap();
            CreateMap<BusinessCaseForm, BusinessCase>().ReverseMap();
            CreateMap<ProjectJustification, ProjectJustificationForm>().ReverseMap();
            CreateMap<Gate, GateForm>().ReverseMap();
            CreateMap<PostLaunchReview, PostLaunchReviewForm>().ReverseMap();
            CreateMap<FinancialData, FinancialDataForm>().ReverseMap();
            CreateMap<Mitigation, MitigationForm>().ReverseMap();
            CreateMap<LiteStageConfigForm, LiteStageConfig>().ReverseMap();

            
            // stage subclasses
            CreateMap<ProjectJustificationNav, ProjectJustification>().ReverseMap();
            CreateMap<BusinessCaseNav, BusinessCase>().ReverseMap();
            CreateMap<ProductInfrigmentPatentabilityNav, ProductInfrigmentPatentability>().ReverseMap();
            CreateMap<KeyCharacteristicNav, KeyCharacteristic>().ReverseMap();
            CreateMap<CustomerDesignApprovalNav, CustomerDesignApproval>().ReverseMap();
            CreateMap<InvestmentPlanNav, InvestmentPlan>().ReverseMap();
            CreateMap<ProductIntroChecklistNav, ProductIntroChecklist>().ReverseMap();
            CreateMap<PostLaunchReviewNav, PostLaunchReview>().ReverseMap();


            //Reports 
            CreateMap<Report_Project, ProjectDetail>().ReverseMap();
            CreateMap<Report_BusinessCase, BusinessCase>().ReverseMap();
            CreateMap<Report_FinancialData, FinancialData>().ReverseMap();
            CreateMap<Report_ProjectSalesRegion, ProjectDetail_SalesRegion>().ReverseMap();


        }
    }
}
