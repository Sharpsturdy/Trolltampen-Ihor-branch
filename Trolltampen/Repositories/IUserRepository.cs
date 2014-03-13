using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trolltampen.Models;

namespace Trolltampen.Repositories
{
    public interface IUserRepository:IDisposable
    {
        void Create(CreateUserProfileModel user);
        void Update(UserProfile user);
        void Delete(int userID);
        UserProfile GetUserByID(int userID);
        IEnumerable<UserProfile> GetAllUsers();
        void ChangeActiveStatus(int userID, bool isActive);
        bool IsUserActive(string userName);
        bool IsUserNameExist(string userName);
        bool IsEmailExist(string email);
        string GetUserNamebyEmail(string email);
    }
}
