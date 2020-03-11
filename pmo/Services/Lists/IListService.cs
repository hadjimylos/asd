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
        SelectList Tags_SelectList(string CategoryKey);
        SelectList Tags_SelectList(String CategoreyKey ,String id);

        MultiSelectList Tags_MultiSelectList(String CategoreyKey);
        MultiSelectList Tags_MultiSelectList<T>(String CategoreyKey , IEnumerable<T> ids);
        List<SelectListItem> Users();
        List<SelectListItem> Users(List<int> selected);
        SelectList ProjectProcess();

    }
}
