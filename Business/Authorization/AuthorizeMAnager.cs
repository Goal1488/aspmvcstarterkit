using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Business.Stores;
using Entities.Users;
using FuckThisNumber.Interfaces;
using FuckThisNumber.Interfaces.Repository;
using Models.Models;
using Models.ViewModels;
using Models.ViewModels.Authentificate;

namespace Business.Authorization
{
    public class AuthorizeManager : IAuthorizeManager
    {
        private readonly UserStore _userStore;
        public IAsyncRepository _repository { get; set; }

        public AuthorizeManager(IAsyncRepository repository)
        {
            _userStore = new UserStore(repository);
        }

        public IAuthentificateResult Authentificate(ILoginViewModel loginViewModel)
        {
            var user = _userStore.GetUserByEmail(loginViewModel.Email);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);

                var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(20), false, "User");
                var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                HttpContext.Current.Response.Cookies.Add(authCookie);

                return new AuthentificateResult()
                {
                    Authorized = true
                };
            }
            else
            {
                return new AuthentificateResult()
                {
                    Authorized = false
                };
            }
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }

    }

}
