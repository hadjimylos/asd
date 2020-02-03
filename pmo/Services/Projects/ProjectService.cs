using AutoMapper;
using Microsoft.AspNetCore.Http;
using pmo.Controllers;
using dbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
namespace pmo.Services.Projects
{
    public class ProjectService : BaseController, IProjectService
    {
        public ProjectService(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
           
        }
        public List<ProjectDetail> GetAllVBPDProjectDetailList()
        {
            var projects =  _context.ProjectDetails.Include(p => p.Project)
                .Include(t => t.ProjectClassification)
                .Include(t => t.ProductLine)
                .Include(t => t.ProjectCategory)
                .Include(t => t.DesignAuthority).ToList();
            return projects;
        }

        public bool AddNewVBPDProject(VBPDViewModel model) {
            var projecInserted = false;
            try
            {
                var projectDetail = _mapper.Map<ProjectDetail>(model);
                var project = _mapper.Map<Project>(model);
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Projects.Add(project);
                    _context.SaveChanges();
                    projectDetail.ProjectId = project.Id;
                    _context.ProjectDetails.Add(projectDetail);
                    _context.SaveChanges();

                    model.CustomerIds.ForEach(customerId =>
                    {
                        _context.ProjectDetail_Customers.Add(new ProjectDetail_Customer{
                            CustomersTagId = customerId,
                            ProjectDetailId = projectDetail.Id,
                        });
                    });
                    model.SalesRegionIds.ForEach(endUserCountryTagId =>{
                        _context.ProjectDetail_SalesRegions.Add(new ProjectDetail_SalesRegion
                        {
                            SalesRegionTagId = endUserCountryTagId,
                            ProjectDetailId = projectDetail.Id,
                        });
                    });
                    _context.SaveChanges();
                    transaction.Commit();
                    projecInserted = true;
                }
                return projecInserted;
            }
            catch (System.Exception ex)
            {
                //logger
                return projecInserted;
            }
            
        }
    }
}