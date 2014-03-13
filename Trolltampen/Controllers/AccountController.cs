using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trolltampen.Models;
using WebMatrix.WebData;
using Trolltampen.Repositories;
using Trolltampen.DAL;
using Trolltampen.App_Code;

namespace Trolltampen.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LoginModel());
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel lm)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(lm.Name, lm.Password, lm.RememberMe))
                {
                    using (IUserRepository repo = new UserRepository(new TTDBContext()))
                    {
                        if (!repo.IsUserActive(lm.Name))
                        {
                            WebSecurity.Logout();
                            lm.LoginFailedResult = "Your account is blocked";
                            return View(lm);
                        }
                    }
                    return Redirect(Request.QueryString["ReturnUrl"] ?? "/");
                };
                lm.LoginFailedResult = "Wrong Name or Password";
                return View(lm);
            }
            return View(lm);
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Reset()
        {
            return View(new ResetPasswordModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reset(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                using (IUserRepository repo = new UserRepository(new TTDBContext()))
                {
                    string userName = repo.GetUserNamebyEmail(model.Email);
                    if (string.IsNullOrEmpty(userName))
                    {
                        model.ErrorMessage = "This email is not found";
                        return View(model);
                    }
                    string token = WebSecurity.GeneratePasswordResetToken(userName);
                    string app = System.Web.HttpContext.Current.Request.Url.Host;
                    ISendRecoveryLink mailsend = new MailService(token, model.Email, userName, app);
                    System.Threading.Tasks.Task.Run(() => { mailsend.SendLink(); });
                    return RedirectToAction("Login");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult MailLink(string token)
        {
            return View(new TokenPasswordModel() { Token = token });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MailLink(TokenPasswordModel m)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.ResetPassword(m.Token, m.NewPassword);
                bool au = WebSecurity.IsAuthenticated;
                return RedirectToAction("Login");
            }
            return View(m);
        }

    }
}