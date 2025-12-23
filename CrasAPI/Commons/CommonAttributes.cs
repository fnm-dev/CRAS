using Microsoft.AspNetCore.Authorization;

namespace CrasAPI.Commons
{
    public class CommonAttributes
    {
        public class HasPermissionAttribute : AuthorizeAttribute
        {
            public HasPermissionAttribute(string permission)
            {
                Policy = $"Permission:{permission}";
            }
        }
    }
}
