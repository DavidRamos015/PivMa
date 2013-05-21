using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using FacebookLogin;
using FluentSecurity;
using MiniAmazon.Data;
using MiniAmazon.Web.Controllers;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Infrastructure
{
    public static class FluentSecurityConfig
    {
        public static void Configure()
        {
            SecurityConfigurator.Configure(configuration =>
            {
                configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);

                configuration.ForAllControllers().DenyAnonymousAccess();
                // configuration.ForAllControllers().RequireRole(new object[] { Utility.UserRole });


                configuration.For<ErrorsController>().Ignore();


                configuration.For<ConfirmationsController>(x => x.Create_Record()).Ignore();
                configuration.For<FbAccountController>(x => x.Login()).Ignore();
                configuration.For<DashBoardController>(x => x.Index()).Ignore();
                //configuration.For<DashBoardController>(x => x.SimpleFilter(new SearchFilterInputModel())).Ignore();
                configuration.For<DashBoardController>(x => x.SimpleFilter(new SearchFilterInputModel())).Ignore();
                configuration.For<MyAccountController>(x => x.SignIn("")).Ignore();
                configuration.For<MyAccountController>(x => x.Create_Record()).Ignore();
                configuration.For<MyAccountController>(x => x.PasswordRecovery()).Ignore();


                configuration.For<ManagementController>().Ignore();
                configuration.For<ManagementController>(x => x.Menu()).RequireRole(new object[] { Utility.AdminRole });

                configuration.For<CategoriesController>().RequireRole(new object[] { Utility.AdminRole });

                configuration.For<ProductController>(x => x.GetPendingApprovalProductList()).RequireRole(new object[] { Utility.AdminRole });
                configuration.For<MyAccountController>(x => x.UserAdminControl()).RequireRole(new object[] { Utility.AdminRole });


                configuration.ResolveServicesUsing(type =>
                {
                    if (type == typeof(IPolicyViolationHandler))
                    {
                        var types = Assembly
                            .GetAssembly(typeof(MvcApplication))
                            .GetTypes()
                            .Where(x => typeof(IPolicyViolationHandler).IsAssignableFrom(x)).ToList();

                        var handlers = types.Select(t => Activator.CreateInstance(t) as IPolicyViolationHandler).ToList();

                        return handlers;
                    }
                    return Enumerable.Empty<object>();
                });
            });

        }
    }
}