using AutoMapper;
using dbModels;
using Microsoft.EntityFrameworkCore;
using pmo.Controllers;
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
            return _context.Users.Include(x => x.Citizenships).ThenInclude(z=>z.Citizenships).Include(y => y.Role).Where(x=>x.Id==userId).FirstOrDefault();
            
            
        }
        public bool AddNewUser(UserViewModel userViewModel)
        {
            userViewModel.isCreate = true;
            var domainModel = _mapper.Map<User>(userViewModel);
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
        public bool UpdateUser(UserViewModel userViewModel)
        {
            userViewModel.isCreate = false;
            var user = _context.Users.Include(x => x.Citizenships).Include(x => x.Role).AsNoTracking().Where(x => x.Id == userViewModel.Id).FirstOrDefault();
            userViewModel.NetworkUsername = user.NetworkUsername;
            var domainModel = _mapper.Map<User>(userViewModel);

            _context.Users.Update(domainModel);
            _context.SaveChanges();
            _context.Entry(domainModel).State = EntityState.Detached;

            List<int> Existing = user.Citizenships.Select(x => x.TagId).ToList();
            List<User_CitizenShip> ExistingModel = user.Citizenships;
            var SameElements = Existing.All(userViewModel.UserCitizenships.Contains);
            var SameCount = Existing.Count == userViewModel.UserCitizenships.Count;
            if (!(SameElements && SameCount))
            {
                _context.UserCitizenShip.RemoveRange(ExistingModel);

                var citizenships = new List<User_CitizenShip>();
                foreach (var item in userViewModel.UserCitizenships)
                {
                    citizenships.Add(new User_CitizenShip() { UserId = domainModel.Id, TagId = item });
                }
                _context.UserCitizenShip.AddRange(citizenships);
            }
            _context.SaveChanges();
            return true;
        }

    }
}
