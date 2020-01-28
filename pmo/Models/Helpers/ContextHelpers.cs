using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace ViewModels.Helpers {
    public static class ActiveDirectoryHelper { 
        public static DirectoryEntry GetUsersManager(string network_username) {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "GLOBAL")) {
                using (var foundUser = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, network_username)) {
                    var user = (DirectoryEntry)foundUser.GetUnderlyingObject();
                    return user.Properties["manager"].Value == null ? null : new DirectoryEntry("LDAP://" + user.Properties["manager"].Value.ToString());
                }
            }
        }

        public static UserPrincipal GetUser(string network_username) {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "GLOBAL")) {
                using (var foundUser = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, network_username)) {
                    return foundUser;
                }
            }
        }
    }

    public static class TagCategoryHelper {
        public const string CitizenshipsKey = "Citizenships";
    }
}
