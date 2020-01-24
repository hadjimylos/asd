using AutoMapper;
using dbModels;
using Microsoft.EntityFrameworkCore;
using pmo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace pmo.Services.Users
{
    public class UserService: BaseController, IUserService
    {
        public UserService(EfContext context, IMapper mapper) : base(context, mapper)
        { 
        
        }
        public User GetUserById(int userId){
          return   _context.Users.Include(x => x.Citizenships).Include(x => x.Role).Where(x => x.Id == userId).FirstOrDefault();
        }
        public bool AddNewUser(UserViewModel userViewModel)
        {
            var domainModel = _mapper.Map<dbModels.User>(userViewModel);
            _context.Users.Add(domainModel);
            _context.SaveChanges();

            var citizenships = new List<User_CitizenShip>();
            foreach (var item in userViewModel.UserCitizenships)
            {
                citizenships.Add(new User_CitizenShip() { UserId = domainModel.Id, TagId = item });
            }
            _context.UserCitizenShip.AddRange(citizenships);
            _context.SaveChanges();
            return true;
        }

    }
}
