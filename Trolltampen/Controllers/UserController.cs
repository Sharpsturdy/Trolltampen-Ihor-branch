using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trolltampen.DAL;
using Trolltampen.Repositories;
using Trolltampen.Models;
using WebMatrix.WebData;

namespace Trolltampen.Controllers
{
    [Authorize]
    public class UserController:Controller
    {
        protected IUserRepository userRepo;
        public UserController()
        {
            userRepo = new UserRepository(new TTDBContext());
        }
        public UserController(IUserRepository iUserRepo)
        {
            this.userRepo = iUserRepo;
        }

        public ViewResult Index()
        {
            return View(userRepo.GetAllUsers());
        }

        public ViewResult EditUser(int uID)
        {
            UserProfile user=userRepo.GetUserByID(uID);
            if (user!=null)
            {
                return View(new EditUserModel() 
                {
                    UserName=user.UserName,
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    Email=user.Email,
                    ID=user.UserId
                });
            }
            return View(new EditUserModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(EditUserModel user)
        {
            if(ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(user.NewPassword) && !string.IsNullOrEmpty(user.OldPassword))
                {
                    if (!WebSecurity.ChangePassword(WebSecurity.CurrentUserName, user.OldPassword, user.NewPassword))
                    {
                        user.ErrorMessage = "Wrong current password";
                        return View("EditUserNoAjax", user);
                    }
                }
                userRepo.Update(new UserProfile()
                    {
                        UserName=user.UserName,
                        FirstName=user.FirstName,
                        LastName=user.LastName,
                        Email=user.Email,
                        UserId=user.ID
                    });
                return RedirectToAction("Index");
            }
            return View("EditUserNoAjax", user);
        }

        public ActionResult ActivateUser(int uID, bool toActivate)
        {
            userRepo.ChangeActiveStatus(uID, toActivate);
            return View("UsersTable",userRepo.GetAllUsers());
        }

        public ActionResult CreateUser()
        {
            return View(new CreateUserProfileModel());
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(CreateUserProfileModel user)
        {
            if (ModelState.IsValid)
            {
                userRepo.Create(user);
                WebMatrix.WebData.WebSecurity.CreateAccount(user.UserName, user.Password);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult DeleteUSer(int uID)
        {
            userRepo.Delete(uID);
            return View("UsersTable", userRepo.GetAllUsers());
        }

        protected override void Dispose(bool disposing)
        {
            userRepo.Dispose();
            base.Dispose(disposing);
        }
	}
}