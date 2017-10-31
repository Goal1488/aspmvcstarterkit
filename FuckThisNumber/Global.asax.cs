using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Business.Authorization;
using Business.Repository;
using Common;
using FuckThisNumber.Interfaces;
using FuckThisNumber.Interfaces.Repository;
using Microsoft.Owin.Infrastructure;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace FuckThisNumber
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SimpleInjectorRegister();
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
        }



        private void SimpleInjectorRegister()
        {
            SimpleInjectorContainer.Default.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            SimpleInjectorInitilize();
            SimpleInjectorContainer.Default.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            SimpleInjectorContainer.Default.RegisterMvcIntegratedFilterProvider();
            SimpleInjectorContainer.Default.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(SimpleInjectorContainer.Default));
            
        }
        
        private void SimpleInjectorInitilize()
        {
            SimpleInjectorContainer.Default.Register<IAuthorizeManager, AuthorizeManager>(Lifestyle.Scoped);

            //repository
            SimpleInjectorContainer.Default.Register<IAsyncRepository>(() => RepositoryAccessor.GetRepository(), Lifestyle.Scoped);


        }
    }
}
