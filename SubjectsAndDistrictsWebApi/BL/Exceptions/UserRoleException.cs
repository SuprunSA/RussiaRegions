using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Exceptions
{
    public class UserRoleException : Exception
    {
        public UserRoleException(string mes) : base(mes) { }
    }
}
