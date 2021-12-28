using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SubjectsAndDistrictsDbContext.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Auth
{
    public class IdentityDataInitializer
    {
        readonly IConfiguration configuration;
        readonly RoleManager<IdentityRole> roleManager;
        readonly UserManager<UserDbDTO> userManager;

        public IdentityDataInitializer(IConfiguration configuration, RoleManager<IdentityRole> roleManager, UserManager<UserDbDTO> userManager)
        {
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task InitializeAsync()
        {
            await AddRole("Admin");

            var defaultUsers = configuration
                                    .GetSection("DefaultUsers")
                                    .GetChildren();

            foreach (var user in defaultUsers)
            {
                await AddUser(user["UserName"], user["Email"], user["Password"], user["FirstName"], user["MiddleName"], user["LastName"], user["Roles"].Split(','));
            }
        }

        public async Task AddRole(string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        public async Task AddUserToRole(UserDbDTO userDB, string role)
        {
            if (userDB != null && !await userManager.IsInRoleAsync(userDB, role))
                await userManager.AddToRoleAsync(userDB, role);
        }

        public async Task AddUser(string userName, string email, string password, string firstName, string middleName, string lastName, IEnumerable<string> roles)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new UserDbDTO()
                {
                    UserName = userName,
                    Email = email,
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, password);
            }
            foreach (var role in roles)
            {
                await AddUserToRole(user, role);
            }
        }
    }
}
