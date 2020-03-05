using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pmo;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
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

        public static void UpdateRelated<T, T2>(this List<T> manyTable, IQueryable<T2> allCurrentRecordsBasedOnParentTable, EfContext context)
            where T : DatabaseModel
            where T2 : DatabaseModel {

            var updateIds = manyTable
                .Where (w => w.Id != 0)
                .Select(s => s.Id)
                .ToList();

            // remove unused
            var deletes = allCurrentRecordsBasedOnParentTable.Where (
                    w =>
                        !updateIds.Contains(w.Id)
                );
            context.RemoveRange(deletes);

            // update existing
            var updates = manyTable.Where(w => updateIds.Contains(w.Id)).ToList();
            updates.ForEach(update => {
                var trackedTable = context.FinancialData.IncludeAll().First(f => f.Id == update.Id);

                // use reflection to set all non virtual properties of tracked table to new values
                trackedTable.GetType()
                    .GetProperties().Where(
                        w =>
                            !w.GetGetMethod().IsVirtual
                    ).ToList().ForEach(f => {
                        var newVal = update.GetType().GetProperty(f.Name).GetValue(update);
                        f.SetValue(trackedTable, newVal);
                    });
            });

            // instert missing
            var inserts = manyTable.Where(w => w.Id == 0).ToList();
            context.AddRange(inserts);

            context.SaveChanges();
        }

        public static void PrepForUpdate<T1, T2>(this T1 copyTo, T2 copyFrom)
            where T1 : class
            where T2 : class {
            
            var typeFrom = typeof(T2);
            var typeTo = typeof(T1);

            if (typeFrom != typeTo)
                throw new Exception("You may not use CopyToTrackedMemory on two objects that are different");

            var fromProps = typeFrom.GetProperties().OrderBy(o => o.Name).ToList();
            var toProps = typeTo.GetProperties().OrderBy(o => o.Name).ToList();

            fromProps.ForEach(from => {
                var to = toProps.First(f => f.Name == from.Name);
                var fromValue = from.GetValue(copyFrom);
                var toValue = to.GetValue(copyTo);

                var isNotNullObject =
                    Type.GetTypeCode(fromValue?.GetType()) == TypeCode.Object ||
                    Type.GetTypeCode(toValue?.GetType()) == TypeCode.Object;

                // don't override if not null object (entity framework will crash if overwriting object with null on save)
                if(!isNotNullObject && to.Name.ToLower() != "id")
                    to.SetValue(copyTo, fromValue);
            });
        }

        public static T GetLatestVersion<T>(this IQueryable<T> queryable, int projectId) where T : StageHistoryModel {
            return  queryable
                 .Include(p => p.Stage)
                 .OrderByDescending(c => c.CreateDate)
                 .Where(w => w.Stage.ProjectId == projectId).FirstOrDefault();
        }

        public static string GetRouteValue (HttpRequest request, string key) {
            return (string)request.RouteValues[key];
        }

        public static List<T> RemoveTransactions<T>(this IEnumerable<T> listItems) where T : HistoryModel {
            var groupedByVersion = listItems.OrderBy(o => o.CreateDate)
                .GroupBy(f => f.Version)
                .ToList();
            var latestTransactionsOnly = groupedByVersion.Select(s => s.Last()).ToList();
            return latestTransactionsOnly.ToList();
        }

        public static List<T> DistinctProjectDetail<T>(this IQueryable<T> listDetails) where T : ProjectDetail {
            return listDetails.ToList().GroupBy(s => s.ProjectId)
                .Select(
                    s =>
                        s.OrderByDescending(x => x.CreateDate).First()
                ).ToList();
        }

        public static string GetDelimited(List<string> items) {
            if (items.Count == 0)
                return string.Empty;
            if (items.Count == 1)
                return items.First();

            var withDelimiter = items.Select(s => $"{s}, ");
            var split = string.Join(string.Empty, withDelimiter);
            return split.Remove(split.Length - 2);
        }

        public static string GetFriendlyKeyName(string key) {
            string keyName = key.Replace("-", " ");
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(keyName);
        }
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
        public const string StageFiles = "stage-files";
        
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