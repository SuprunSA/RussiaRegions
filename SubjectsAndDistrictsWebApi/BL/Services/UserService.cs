using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SubjectsAndDistrictsDbContext.Model.DTO;
using SubjectsAndDistrictsWebApi.BL.Exceptions;
using SubjectsAndDistrictsWebApi.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Services
{
    public class UserService
    {
        private readonly UserManager<UserDbDTO> userManager;

        public UserService(UserManager<UserDbDTO> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> UserExists(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return user != null;
        }

        #region Manage
        public async Task<IEnumerable<UserApiDTO>> GetProfiles()
        {
            var users = await userManager.Users.ToListAsync();
            return users.Select(u => new UserApiDTO(u, userManager.GetRolesAsync(u).Result));
        }

        public async Task<UserApiDTO> GetProfile(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null) return null;
            var userProfile = new UserApiDTO(user);
            userProfile.Roles = await userManager.GetRolesAsync(user);
            return userProfile;
        }

        public async Task<Exception> UpdateProfile(UserApiDTO userApi)
        {
            var userDB = await userManager.FindByNameAsync(userApi.UserName);
            if (userDB == null) return new KeyNotFoundException(string.Format("Пользователь с именем {0} не найден", userApi.UserName));
            userApi.Update(userDB);
            var result = await userManager.UpdateAsync(userDB);
            if (result.Succeeded) return null;
            else return new SaveChangesException();
        }

        public async Task<Exception> ResetPassword(string userName, string newPassword)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null) return new KeyNotFoundException(string.Format("Пользователь с именем {0} не найден", userName));
            else
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, newPassword);
                if (result.Succeeded) return null;
                else return new SaveChangesException();
            }
        }

        public async Task<Exception> CreateProfile(UserApiDTOCreate userApi)
        {
            var profile = userApi.Create();
            var result = await userManager.CreateAsync(profile, userApi.Password);
            if (!result.Succeeded)
            {
                if (await UserExists(userApi.UserName)) return new AlreadyExistException(string.Format("Пользователь с именем {0} уже существует", userApi.UserName));
                else return new CreateUserException();
            }
            result = await userManager.AddToRolesAsync(profile, userApi.Roles);
            if (result.Succeeded) return null;
            else return new UserRoleException(string.Format("Не удалось добавить роли пользователю {0}", userApi.UserName));
        }

        public async Task<Exception> Delete(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null) return new KeyNotFoundException(string.Format("Пользователь с именем {0} не найден", userName));
            else
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded) return null;
                else return new SaveChangesException();
            }
        }
        #endregion

        #region Roles
        public async Task<Exception> AddToRole(string userName, string role)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null) return new KeyNotFoundException(string.Format("Пользователь с именем {0} не найден", userName));
            else
            {
                var result = await userManager.AddToRoleAsync(user, role);
                if (result.Succeeded) return null;
                else return new UserRoleException(string.Format("Не удалось назначить пользователю {0} роль {1}", userName, role));
            }
        }

        public async Task<Exception> RemoveRole(string userName, string role)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null) return new KeyNotFoundException(string.Format("Пользователь с именем {0} не найден", userName));
            else
            {
                var result = await userManager.RemoveFromRoleAsync(user, role);
                if (result.Succeeded) return null;
                else return new UserRoleException(string.Format("Не удалось удалить пользователю {0} роль {1}", userName, role));
            }
        }
        #endregion
    }
}
