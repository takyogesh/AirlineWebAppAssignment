using Microsoft.AspNetCore.Mvc.Filters;

namespace AirlineWebApp.customMiddleware
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate| AttributeTargets.All)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly  string allowedroles;
        public CustomAuthorizeAttribute(string roles)
        {
            this.allowedroles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (string.IsNullOrWhiteSpace(context.HttpContext.Session.GetString("Name")))
            {
                context.HttpContext.Response.Redirect("Account/Index");
            }
            else
            {
               
                var role = context.HttpContext.Session.GetString("role");
                
                if (role != null)
                {
                    if (allowedroles != role)
                    {
                       context.HttpContext.Response.Redirect("Account/ErrorPage");
                    }
                }
                else
                {
                    context.HttpContext.Response.Redirect("Account/login");
                }
            }
        }
    }
}
