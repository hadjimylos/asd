using System.Collections.Generic;

namespace ViewModels
{
    public class AdminViewModel {
        public List<StageConfigViewModel> StageConfigViewModel { get; set; }
        
        public List<GateConfigViewModel> GateConfigViewModel { get; set; }
        
        public List<RoleViewModel> RolesViewModel  { get; set;}
        
        public List<TagViewModel> TagViewModel { get; set; }

        public List<forms.UserForm> UserViewModel { get; set; }
    }
}
