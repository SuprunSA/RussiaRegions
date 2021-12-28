using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsDbContext.Repositories
{
    public class SNDRepositoryBase
    {
        protected SubjectsAndDistrictsContext context;

        public SNDRepositoryBase(SubjectsAndDistrictsContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
