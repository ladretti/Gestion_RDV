using Microsoft.AspNetCore.Mvc;

namespace Gestion_RDV.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserAuthorizeAttribute : TypeFilterAttribute
    {
        public UserAuthorizeAttribute(string userIdRouteKey) : base(typeof(UserAuthorizationFilter))
        {
            Arguments = new object[] { userIdRouteKey };
        }
    }
}
