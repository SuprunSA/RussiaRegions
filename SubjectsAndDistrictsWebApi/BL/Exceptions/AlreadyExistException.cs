using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Exceptions
{
    public class AlreadyExistException : ArgumentException
    {
        public AlreadyExistException() : base("Сущность с таким кодом уже существует") { }
        public AlreadyExistException(string mes) : base(mes) { }
    }
}
