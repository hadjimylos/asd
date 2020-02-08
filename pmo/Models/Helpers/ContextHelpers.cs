using Microsoft.EntityFrameworkCore;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ViewModels.Helpers
{
    public static class Helpers {
        // extend IQueryable to Include all virtual objects
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable) where T : class {
            var type = typeof(T);
            var properties = type.GetProperties();
            
            // get all virtual properties of this db object
            var virtualProperties = properties.Where (
                w => w.GetGetMethod().IsVirtual
            ).ToList();

            // append Include to each of these properties in queryable
            virtualProperties.ForEach(f => {
                queryable = queryable.Include(f.Name);

                // get enumerable lists and run self for each of these methods
                // specifically for reverse navigation items (e.g. List<related_object>)
                if (f.PropertyType.FullName.Contains("List")) {
                    string childProperty = f.PropertyType.GetGenericArguments().First().FullName;
                    var navProperty = Type.GetType(childProperty);
                    var navProperties = navProperty.GetProperties();

                    // get all virtual properties of this db object
                    var navVirtualProperties = navProperties.Where(
                        w => 
                            w.GetGetMethod().IsVirtual &&
                            w.Name != type.Name // do not load self each time
                    ).ToList();

                    navVirtualProperties.ForEach(navProperty => {
                        queryable = queryable.Include($"{f.Name}.{navProperty.Name}");
                    });
                }
            });

            return queryable;
        }

   

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
        public const string Customer = "customers";
        public const string ProductLine = "product-line";
        public const string RiskType = "risk-type";
        public const string RiskProbability = "risk-probability";
        public const string RiskImpact = "risk-impact";
        public const string DesignAuthority = "design-authority";
        public const string ExportApplicationType = "export-application-type";
    }

    public static class JobDescripKeys {
        public const string ProgramManager = "program-manager";
        public const string ProductManager = "product-manager";
        public const string LeadEngineer = "lead-engineer";
        public const string ProgramManagement = "program-management";
        public const string ProductEngineering = "product-engineering";
        public const string AdvancedTechnology = "advanced-technology";
        public const string Sales = "sales";
        public const string IndustrySegment = "industry-segment";
        public const string Operations = "operations";
        public const string ManufacturingEngineering = "manufacturing-engineering";
        public const string Planning = "planning";
        public const string Sourcing = "sourcing";
        public const string Quality = "quality";
        public const string LaboratoryTesting = "laboratory-testing";
        public const string Finance = "finance";
    }
}