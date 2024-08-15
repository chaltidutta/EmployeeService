using UserService.DTOS;
using UserService.Models;

namespace UserService.ServiceLayer
{
    public interface IUserService
    {
        bool RegisterUser(User user);
        User ValidateUser(UserLoginDTO user);
        List<User> GetAllUsers();
        User GetUserById(int id);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
    }
}
