using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Exceptions
{
    public class SaveChangesException : Exception
    {
        public SaveChangesException() : base("Ошибка при сохранении данных") { }
    }
}
