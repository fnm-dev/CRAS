using Microsoft.AspNetCore.Authorization;

namespace CrasAPI.Commons.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (context.User.HasClaim("permissions", requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
