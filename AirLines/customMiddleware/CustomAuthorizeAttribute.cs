using AirLines.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AirLines.customMiddleware
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate| AttributeTargets.All)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly  string allowedroles;
        

        public CustomAuthorizeAttribute(string roles)
        {
            this.allowedroles = roles;
          
        }
        



        public void OnAuthorization(AuthorizationFilterContext httpcontext)
        {

            if (string.IsNullOrWhiteSpace(httpcontext.HttpContext.Session.GetString("Name")))
            {
                httpcontext.HttpContext.Response.Redirect("Account/Index");
            }
            else
            {
               
                var role = httpcontext.HttpContext.Session.GetString("role");
                
                if (role != null)
                {
                    if (allowedroles == role)
                    {
                        if (allowedroles == "Admin" && role == "Admin")
                        {
                            //httpcontext.HttpContext.Response.Redirect("/Admin/Index");
                            //redi
                        }
                        else if (allowedroles == "Operator" && role == "Operator")
                        {
                            //httpcontext.HttpContext.Response.Redirect("/Operator/Index");
                        }
                    }
                    else
                    {
                        httpcontext.HttpContext.Response.Redirect("Account/ErrorPage");

                    }


                }
                else
                {
                    httpcontext.HttpContext.Response.Redirect("Account/login");
                }
            }
            
            
        }
    }
}
