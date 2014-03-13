using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trolltampen.Models;
using Trolltampen.DAL;

namespace Trolltampen.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected TTDBContext db;
        protected bool disposed = false;
        public UserRepository(TTDBContext db)
        {
            this.db = db;
        }
        public void Create(CreateUserProfileModel user)
        {
            if (user != null)
            {
                db.UserProfiles.Add(new UserProfile()
                {
                    UserName=user.UserName,
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    Email=user.Email,
                    IsActive=true
                });
                db.SaveChanges();
              
            }
        }
        public void Update(UserProfile user)
        {
            if (user != null)
            {
                UserProfile edituser = db.UserProfiles.FirstOrDefault(u => u.UserId == user.UserId);
                if (edituser != null)
                {
                    edituser.UserName = user.UserName;
                    edituser.FirstName = user.FirstName;
                    edituser.LastName = user.LastName;
                    edituser.Email = user.Email;
                    db.SaveChanges();
                }
            }
        }
        public void Delete(int userID)
        {
            UserProfile deluser = db.UserProfiles.Find(userID);
            if (deluser != null)
            {
                db.UserProfiles.Remove(deluser);
                Membership mu = db.Memberships.Find(userID);
                if (mu!=null)
                {
                    db.Memberships.Remove(mu);
                }
                db.SaveChanges();
            }
        }
        public UserProfile GetUserByID(int userID)
        {
            UserProfile user = db.UserProfiles.Find(userID);
            return user;
        }

        public IEnumerable<UserProfile> GetAllUsers()
        {
            return db.UserProfiles;
        }
        public void ChangeActiveStatus(int userID, bool isActive)
        {
            UserProfile user = db.UserProfiles.Find(userID);
            if (user == null) return;
            user.IsActive = isActive;
            db.SaveChanges();
        }

        public bool IsUserActive(string userName)
        {
            UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName == userName);
            if (user == null) return false;
            return user.IsActive;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsUserNameExist(string userName)
        {
            return db.UserProfiles.Any(n=>n.UserName==userName);
        }

        public bool IsEmailExist(string email)
        {
            return db.UserProfiles.Any(u => u.Email == email);
        }


        public string GetUserNamebyEmail(string email)
        {
            UserProfile user = db.UserProfiles.FirstOrDefault(u => u.Email == email);
            if (user==null)
            {
                return null;
            }
            return user.UserName;
        }
    }
}