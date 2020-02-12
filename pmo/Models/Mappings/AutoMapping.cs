using AutoMapper;
using dbModels;
using ViewModels;

namespace pmo.Models.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserViewModel>().ReverseMap().ReverseMap();
            CreateMap<StageConfig, StageConfigViewModel>().ReverseMap();
            CreateMap<GateConfig, GateConfigViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<Tag, TagViewModel>().ReverseMap();
            CreateMap<TagCategory, TagCategoryViewModel>().ReverseMap();
            CreateMap<Project, VBPDViewModel>().ReverseMap();
            CreateMap<ProjectDetail, VBPDViewModel>().ReverseMap();
            CreateMap<ProjectDetail, VBPDProjectDetailViewModel>().ReverseMap();
            CreateMap<CustomerDesignApproval, CustomerDesignApprovalViewModel>().ReverseMap();
            CreateMap<Schedule, SchedulesViewModel>().ReverseMap();
            CreateMap<InvestmentPlan, InvestmentPlanViewModel>().ReverseMap();
            CreateMap<KeyCharacteristic, KeyCharacteristicViewModel>().ReverseMap();
            CreateMap<ProductIntroChecklist, ProductIntroChecklistViewModel>().ReverseMap();
            CreateMap<Risk, RiskViewModel>().ReverseMap();
            CreateMap<Gate, NavigationGate>().ReverseMap();
            CreateMap<Stage, NavigationStage>().ReverseMap();
            CreateMap<ProductInfrigmentPatentability, ProductInfrigmentPatentabilityViewModel>().ReverseMap();

            // stage subclasses
            CreateMap<ProjectJustificationNav, ProjectJustification>().ReverseMap();
            CreateMap<BusinessCaseNav, BusinessCase>().ReverseMap();
            CreateMap<ProductInfrigmentPatentabilityNav, ProductInfrigmentPatentability>().ReverseMap();
            CreateMap<KeyCharacteristicNav, KeyCharacteristic>().ReverseMap();
            CreateMap<CustomerDesignApprovalNav, CustomerDesignApproval>().ReverseMap();
            CreateMap<InvestmentPlanNav, InvestmentPlan>().ReverseMap();
            CreateMap<ProductIntroChecklistNav, ProductIntroChecklist>().ReverseMap();
            CreateMap<RampResourcePlanNav, RampResourcePlan>().ReverseMap();
            CreateMap<QualificationTestingNav, QualificationTesting>().ReverseMap();
        }
    }
}
