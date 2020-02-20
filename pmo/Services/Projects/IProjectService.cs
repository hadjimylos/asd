using dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace pmo.Services.Projects
{
    public interface IProjectService
    {
        void AddNewVBPDProject(VBPDViewModel model);

    }
}
