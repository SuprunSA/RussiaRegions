using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SubjectsAndDistrictsDbContext.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Auth
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserDbDTO, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<UserDbDTO> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options) { }

        public async override Task<ClaimsPrincipal> CreateAsync(UserDbDTO userDB)
        {
            var principal = await base.CreateAsync(userDB);
            var identity = (ClaimsIdentity)principal.Identity;

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, userDB.Email ?? ""));
            claims.Add(new Claim("FirstName", userDB.FirstName ?? ""));
            claims.Add(new Claim("MiddleName", userDB.MiddleName ?? ""));
            claims.Add(new Claim("LastName", userDB.LastName ?? ""));
            claims.Add(new Claim(ClaimTypes.Role, string.Join(",", await UserManager.GetRolesAsync(userDB)) ?? ""));

            identity.AddClaims(claims);
            return principal;
        }
    }
}
