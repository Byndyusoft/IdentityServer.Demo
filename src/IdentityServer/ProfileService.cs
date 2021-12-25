using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var customGrant = context.Subject.FindFirst("custom_grant");
            if (customGrant != null)
                context.IssuedClaims.Add(new Claim(customGrant.Type, customGrant.Value));

            context.IssuedClaims.AddRange(ClaimService.GetAdditionalClaims(context.Subject.GetSubjectId()));

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(true);
        }
    }

    public class ClaimService
    {
        public static IEnumerable<Claim> GetAdditionalClaims(string subjectId)
        {
            if (subjectId == "818727") // Alice 
            {
                yield return new Claim("wonder_woman", "true");
            }
        }
    }
}