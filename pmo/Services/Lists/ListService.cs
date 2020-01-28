using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pmo.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Services.Lists
{
    public class ListService: BaseController, IListService
    {
        public ListService(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
        
        }

        public SelectList Roles()
        {
           return new SelectList(_context.Roles.ToList(), "Id", "FriendlyName");
        }

        public SelectList Roles(string id)
        {
            return new SelectList(_context.Roles.ToList(), "Id", "FriendlyName", id);
        }

        public SelectList Citizenships()
        {
            return new SelectList(_context.Tags.Include(x => x.TagCategory).Where(x => x.TagCategory.Key == TagCategoryHelper.CitizenshipsKey).ToList(), "Id", "Name");
        }

        public SelectList Citizenships(string id)
        {
            return new SelectList(_context.Tags.Include(x => x.TagCategory).Where(x => x.TagCategory.Key == TagCategoryHelper.CitizenshipsKey).ToList(), "Id", "Name", id);
        }

        public MultiSelectList CitizenshipsMultiple()
        {
            return new MultiSelectList(_context.Tags.Include(x => x.TagCategory).Where(x => x.TagCategory.Key == TagCategoryHelper.CitizenshipsKey).ToList(), "Id", "Name");
        }

        public MultiSelectList CitizenshipsMultiple<T>(IEnumerable<T> selected) 
        {
            return new MultiSelectList(_context.Tags.Include(x => x.TagCategory).Where(x => x.TagCategory.Key == TagCategoryHelper.CitizenshipsKey).ToList(), "Id", "Name", selected);
        }

    }
}
