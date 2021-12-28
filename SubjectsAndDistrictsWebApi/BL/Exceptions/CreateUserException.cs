using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Exceptions
{
    public class CreateUserException : Exception
    {
        public CreateUserException() : base("Не удалось создать пользователя") { }
    }
}
