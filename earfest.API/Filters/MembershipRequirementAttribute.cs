using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Filters
{
    public class MembershipRequirementAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _requiredMembershipType;

        public MembershipRequirementAttribute(string requiredMembershipType)
        {
            _requiredMembershipType = requiredMembershipType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userMembershipType = context.HttpContext.User.FindFirst("MembershipType")?.Value;

            if (userMembershipType == null || userMembershipType != _requiredMembershipType)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
