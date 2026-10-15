using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic; // This is included for List<Claim>

namespace RaceDayAPI.Tests.Helpers
{
    public static class ActionContextHelper
    {
        public static ControllerContext GetContextWithRole(string role)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, role), new Claim(ClaimTypes.Name, "TestUser") };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext { User = user };
            return new ControllerContext { HttpContext = httpContext };
        }

        public static ControllerContext GetUnauthenticatedContext()
        {
            var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity()) };
            return new ControllerContext { HttpContext = httpContext };
        }
    }
}