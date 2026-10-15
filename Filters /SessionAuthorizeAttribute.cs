using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RaceDayAPI.Filters // (Match your namespace to your folder structure, e.g., RaceDayAPI.Filters or just RaceDayAPI)
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string[] _allowedRoles;

        public SessionAuthorizeAttribute(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var userId = session.GetInt32("UserID");
            var role = session.GetString("UserRole");

            if (userId == null || string.IsNullOrEmpty(role))
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Unauthorized. Please log in." });
                return;
            }

            if (_allowedRoles.Length > 0 && !_allowedRoles.Contains(role))
            {
                context.Result = new ObjectResult(new { message = "Forbidden. You do not have permission to access this resource." })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
