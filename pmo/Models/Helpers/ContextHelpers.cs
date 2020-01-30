using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace ViewModels.Helpers
{
    public static class ActiveDirectoryHelper
    {
        public static DirectoryEntry GetUsersManager(string network_username)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "GLOBAL"))
            {
                using (var foundUser = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, network_username))
                {
                    var user = (DirectoryEntry)foundUser.GetUnderlyingObject();
                    return user.Properties["manager"].Value == null ? null : new DirectoryEntry("LDAP://" + user.Properties["manager"].Value.ToString());
                }
            }
        }

        public static UserPrincipal GetUser(string network_username)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "GLOBAL"))
            {
                using (var foundUser = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, network_username))
                {
                    return foundUser;
                }
            }
        }
    }

    public static class TagCategoryHelper
    {
        public const string Citizenships = "citizenships";
        public const string RequiredSchedules = "required-schedules";
        public const string SalesRegion = "sales-region";
        public const string ProjectCategory = "project-category";
        public const string ProjectClassification = "project-classification";
        public const string RequirementSource = "requirement-source";
        public const string ManufacturingLocations = "manufacturing-locations";
        public const string ManufacturingLocationsCustomersStrategic = "strategic-customers";
        public const string CustomersFocus = "focus-customers";
        public const string CustomersTarget = "target-customers";
        public const string CustomersNon_sft = "non-sft-customers";
        public const string ProductLine = "product-line";
        public const string RiskType = "risk-type";
        public const string RiskProbability = "risk-probability";
        public const string RiskImpact = "risk-impact";
        public const string DesignAuthority = "design-authority";


    }
}
