using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace pmo.Services.Lists
{
    public interface IListService
    {
        SelectList Roles();
        SelectList Roles(String id);

        SelectList Citizenships();
        SelectList Citizenships(String id);

        MultiSelectList CitizenshipsMultiple();
       
        MultiSelectList CitizenshipsMultiple<T>(IEnumerable<T> ids);

    }
}
