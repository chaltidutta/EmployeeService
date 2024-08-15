using System.Collections.Generic;
using System.Linq;
using UserService.Models;
using UserService.DTOS;
using Microsoft.EntityFrameworkCore;

namespace UserService.ServiceLayer
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;

        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public bool RegisterUser(User user)
        {
            try
            {
                _userContext.Users.Add(user);
                _userContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public User ValidateUser(UserLoginDTO user)
        {
            return _userContext.Users
                .FirstOrDefault(u => u.Username == user.Username && u.PasswordHash == user.Password);
        }

        public List<User> GetAllUsers()
        {
            return _userContext.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _userContext.Users.Find(id);
        }

        public bool UpdateUser(User user)
        {
            var existingUser = _userContext.Users.Find(user.UserId);
            if (existingUser == null)
                return false;

            _userContext.Entry(existingUser).State = EntityState.Detached;
            _userContext.Users.Attach(user);
            _userContext.Entry(user).State = EntityState.Modified;

            _userContext.SaveChanges();

            return true;
        }

        public bool DeleteUser(int id)
        {
            var user = _userContext.Users.Find(id);
            if (user == null)
                return false;

            _userContext.Users.Remove(user);
            _userContext.SaveChanges();

            return true;
        }
    }
}
