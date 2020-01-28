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
        SelectList Tags_SelectList(String id , String CategoreyKey);

        MultiSelectList Tags_MultiSelectList(String CategoreyKey);
       
        MultiSelectList Tags_MultiSelectList<T>(IEnumerable<T> ids, String CategoreyKey);

    }
}
