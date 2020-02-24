using AutoMapper;
using dbModels;
using System.Collections.Generic;
using ViewModels;

namespace pmo.Models.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, forms.UserForm>().ReverseMap().ReverseMap();
            CreateMap<StageConfig, StageConfigViewModel>().ReverseMap();
            CreateMap<GateConfig, GateConfigViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<Tag, TagViewModel>().ReverseMap();
            CreateMap<TagCategory, TagCategoryViewModel>().ReverseMap();
            CreateMap<Project, forms.VBPDForm>().ReverseMap();
            CreateMap<ProjectDetail, forms.VBPDForm>().ReverseMap();
            CreateMap<ProjectDetail, forms.VBPDProjectDetailForm>().ReverseMap();
            CreateMap<CustomerDesignApproval, forms.CustomerDesignApprovalForm>().ReverseMap();
            CreateMap<Schedule, SchedulesViewModel>().ReverseMap();
            CreateMap<InvestmentPlan, forms.InvestmentPlanForm>().ReverseMap();
            CreateMap<KeyCharacteristic, forms.KeyCharacteristicForm>().ReverseMap();
            CreateMap<ProductIntroChecklist, forms.ProductIntroChecklistForm>().ReverseMap();
            CreateMap<Risk, forms.RiskForm>().ReverseMap();
            CreateMap<Gate, NavigationGate>().ReverseMap();
            CreateMap<Stage, NavigationStage>().ReverseMap();
            CreateMap<ProductInfrigmentPatentability, forms.ProductInfrigmentPatentabilityForm>().ReverseMap();
            CreateMap<BusinessCaseViewModel, BusinessCase>().ReverseMap();
            CreateMap<ProjectJustification, forms.ProjectJustificationForm>().ReverseMap();
            CreateMap<Gate, GateViewModel>().ReverseMap();
            CreateMap<PostLaunchReview, forms.PostLaunchReviewForm>().ReverseMap();
            CreateMap<FinancialData, FinancialDataViewModel>().ReverseMap();

            // stage subclasses
            CreateMap<ProjectJustificationNav, ProjectJustification>().ReverseMap();
            CreateMap<BusinessCaseNav, BusinessCase>().ReverseMap();
            CreateMap<ProductInfrigmentPatentabilityNav, ProductInfrigmentPatentability>().ReverseMap();
            CreateMap<KeyCharacteristicNav, KeyCharacteristic>().ReverseMap();
            CreateMap<CustomerDesignApprovalNav, CustomerDesignApproval>().ReverseMap();
            CreateMap<InvestmentPlanNav, InvestmentPlan>().ReverseMap();
            CreateMap<ProductIntroChecklistNav, ProductIntroChecklist>().ReverseMap();
            CreateMap<PostLaunchReviewNav, PostLaunchReview>().ReverseMap();
        }
    }
}
