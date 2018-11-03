using System.Collections.Generic;
using pindogramApp.Dtos;
using pindogramApp.Entities;

namespace pindogramApp.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
        void AddUserToUserGroup(int userId);
        void AddUserToAdminGroup(int userId);
        User ActiveUser(int id);
        User DeactiveUser(int id); 
         IEnumerable<User> GetAllUnactivatedUsers();

    }
}
