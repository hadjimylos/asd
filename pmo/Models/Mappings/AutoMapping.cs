using AutoMapper;
using dbModels;
using ViewModels;

namespace pmo.Models.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
            CreateMap<User, UserViewModel>().ReverseMap().ReverseMap(); ;
            CreateMap<StageConfig, StageConfigViewModel>().ReverseMap();
            CreateMap<GateConfig, GateConfigViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<Tag, TagViewModel>().ReverseMap();
            CreateMap<TagCategory, TagCategoryViewModel>().ReverseMap();
            CreateMap<Project, VBPDViewModel>().ReverseMap();
            CreateMap<ProjectDetail, VBPDViewModel>().ReverseMap();
        }
    }
}
