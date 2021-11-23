using System;
using System.Collections.Generic;

#nullable disable

namespace SNDDbContext.ModelDB
{
    public partial class District
    {
        public District()
        {
            Subjects = new HashSet<Subject>();
        }

        public long Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
