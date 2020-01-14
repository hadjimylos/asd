using AutoMapper;
using dbModels;
using ViewModels;

namespace pmo.Models.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
            CreateMap<User, UserViewModel>();
            CreateMap<StageConfig, StageConfigViewModel>();
            CreateMap<GateConfig, GateConfigViewModel>();
            CreateMap<Role, RoleViewModel>();
            CreateMap<Tag, TagViewModel>();
        }
    }
}
