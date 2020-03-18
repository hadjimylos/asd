using pmo;
using dbModels;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ViewModels {
    public class VbpdProjectDetail {
        private readonly EfContext _context;

        public VbpdProjectDetail(EfContext context, int projectId) {
            _context = context;
            populate(projectId);
        }

        public VbpdProjectDetail() {
        }
        
        public List<GateComment> GateComments { get; set; }

        public ProjectDetail ProjectDetail { get; set; }

        public List<Project_User> TeamMembers { get; set; }

        private void populate(int projectId) {
            this.ProjectDetail = _context.ProjectDetails
                .IncludeAll()
                .Where(w => w.ProjectId == projectId)
                .OrderByDescending(o => o.CreateDate)
                .First();

            this.TeamMembers = _context.Project_User
                .IncludeAll()
                .Include(i => i.User)
                .Where(
                    w => w.ProjectId == projectId
                ).ToList();

            this.GateComments = _context.GateComments
                .Where(w => w.Gate.ProjectId == projectId)
                .OrderByDescending(o => o.CreateDate).ToList();
        }
    }
}
