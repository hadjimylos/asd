using pmo;
using dbModels;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Helpers;

namespace ViewModels {
    public class VbpdProjectDetail {
        private readonly EfContext _context;

        public VbpdProjectDetail(EfContext context, int projectId) {
            _context = context;
            populate(projectId);
        }

        public VbpdProjectDetail() {
        }

        public ProjectDetail ProjectDetail { get; set; }

        public IEnumerable<IGrouping<string, Project_User>> TeamMembers { get; set; }

        private void populate(int projectId) {
            this.ProjectDetail = _context.ProjectDetails
                .IncludeAll()
                .Where(w => w.ProjectId == projectId)
                .OrderByDescending(o => o.CreateDate)
                .First();

            this.TeamMembers = _context.Project_User
                .IncludeAll()
                .Where (
                    w => w.ProjectId == projectId
                ).ToList()
                .GroupBy(g => g.JobDescriptionKey)
                .ToList();
        }
    }
}
