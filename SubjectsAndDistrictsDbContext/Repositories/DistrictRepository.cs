using Microsoft.EntityFrameworkCore;
using SubjectsAndDistrictsDbContext.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsDbContext.Repositories
{
    public class DistrictRepository : SNDRepositoryBase
    {
        public DistrictRepository(SubjectsAndDistrictsContext context) : base(context) { }

        public async Task<IEnumerable<DistrictDbDTO>> GetAllDistrictsAsync(bool orderAsc, string? orderBy)
        {
            var districts = context.Districts.Include(d => d.Subjects).AsQueryable();
            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy)
                {
                    case "population":
                        districts = DistrictsOrderByPopulation(orderAsc, districts);
                        break;
                    case "square":
                        districts = DistrictsOrderBySquare(orderAsc, districts);
                        break;
                    case "populationDencity":
                        districts = DistrictsOrderByPopulationDencity(orderAsc, districts);
                        break;
                }
            }
            
            return await districts.Include(d => d.Subjects).ToListAsync();
        }

        public async Task<DistrictDbDTO> GetDistrictAsync(uint code)
        {
            return await context.Districts.Include(d => d.Subjects).FirstOrDefaultAsync(d => d.Code == code);
        }

        private static IQueryable<DistrictDbDTO> DistrictsOrderByPopulation(bool orderAsc, IQueryable<DistrictDbDTO> districts)
        {
            return DistrictsOrderBy(orderAsc, districts, d => d.Subjects.Sum(s => s.Population));
        }

        private static IQueryable<DistrictDbDTO> DistrictsOrderBySquare(bool orderAsc, IQueryable<DistrictDbDTO> districts)
        {
            return DistrictsOrderBy(orderAsc, districts, d => d.Subjects.Sum(s => s.Square));
        }

        private static IQueryable<DistrictDbDTO> DistrictsOrderByPopulationDencity(bool orderAsc, IQueryable<DistrictDbDTO> districts)
        {
            return DistrictsOrderBy(orderAsc, districts, d => d.Subjects.Sum(s => s.Population) / d.Subjects.Sum(s => s.Square));
        }

        private static IQueryable<DistrictDbDTO> DistrictsOrderBy<TKey>(bool orderAsc, IQueryable<DistrictDbDTO> districts, Expression<Func<DistrictDbDTO, TKey>> expression)
        {
            if (orderAsc) return districts.OrderBy(expression);
            else return districts.OrderByDescending(expression);
        }

        public void Create(DistrictDbDTO district)
        {
            context.Add(district);
        }

        public void Update(DistrictDbDTO district)
        {
            context.Update(district);
        }

        public void Delete(DistrictDbDTO district)
        {
            context.Remove(district);
        }

        public async Task<bool> DistrictExists(uint code)
        {
            return await context.Districts.AnyAsync(d => d.Code == code);
        }
    }
}
