using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gestion_RDV.Filters
{
    public class UserAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string _userIdRouteKey;

        public UserAuthorizationFilter(string userIdRouteKey)
        {
            _userIdRouteKey = userIdRouteKey;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIdClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

            if (userIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var routeData = context.RouteData.Values[_userIdRouteKey];

            if (routeData == null || !int.TryParse(routeData.ToString(), out int routeUserId) || routeUserId.ToString() != userIdClaim.Value)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
