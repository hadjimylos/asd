using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pmo.Controllers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Services.Users
{
    public class UserService : BaseController, IUserService
    {

        public UserService(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
            
        }
        public User GetUserById(int userId)
        {
            return _context.Users.Include(x => x.Citizenships).ThenInclude(z => z.Citizenships).Include(y => y.Role).Where(x => x.Id == userId).FirstOrDefault();
        }

        public int GetCurrentUserId() {
            var userid = _context.Users.Where(u => u.NetworkUsername.ToLower() == _httpContextAccessor.HttpContext.User.Identity.Name.ToLower()).Select(s => s.Id).FirstOrDefault();
            return userid;
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

        public ValidationResult AD_GetUser(string networkUsername)
        {
            if (!string.IsNullOrEmpty(networkUsername))
            {
                var user = ActiveDirectoryHelper.GetUser(networkUsername);
                if (user == null)
                {
                    return new ValidationResult(ErrorMessages.MissingUserActiveDirectory);
                }
            }
            return ValidationResult.Success;
        }
    }
}