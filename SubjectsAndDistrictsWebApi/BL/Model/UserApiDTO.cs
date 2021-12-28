using SubjectsAndDistrictsDbContext.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Model
{
    public class UserApiDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public UserApiDTO() { }

        public UserApiDTO(UserDbDTO user)
        {
            UserName = user.UserName;
            Email = user.Email;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
        }

        public UserApiDTO(UserDbDTO user, IEnumerable<string> roles) : this(user)
        {
            Roles = roles;
        }

        public void Update(UserDbDTO user)
        {
            user.Email = Email;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.MiddleName = MiddleName;
        }

        public UserDbDTO Create()
        {
            return new UserDbDTO()
            {
                UserName = UserName,
                Email = Email,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
            };
        }  
    }
    
    public class UserApiDTOCreate : UserApiDTO
        {
            public string Password { get; set; }
        }
}
