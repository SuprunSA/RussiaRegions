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
    public class SubjectRepository : SNDRepositoryBase
    {
        public SubjectRepository(SubjectsAndDistrictsContext context) : base(context) { }

        public async Task<IEnumerable<SubjectDbDTO>> GetAllSubjectsAsync(string? filter, bool orderAsc, string? orderBy, bool order)
        {
            var subjects = context.Subjects.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                subjects = subjects
                                .Where(s => EF.Functions.Like(s.Name, $"%{filter}%")); 
            }

            if (!string.IsNullOrEmpty(orderBy) && order)
            {
                subjects = OrderSubjects(orderBy, orderAsc, subjects);
            }

            return await subjects.Include(s => s.District).ToListAsync();
        }
        public async Task<IEnumerable<SubjectDbDTO>> FilterSubjectsAsync(string name, string? orderBy, bool orderAsc, bool order)
        {
            var subjects = context.Subjects.AsQueryable();
            subjects = subjects.Where(s => s.District.Name == name);
            if (!string.IsNullOrEmpty(orderBy) && order)
            {
                subjects = OrderSubjects(orderBy, orderAsc, subjects);
            }
            return await subjects.Include(s => s.District).ToListAsync();
        }

        public async Task<SubjectDbDTO> GetSubjectAsync(uint code)
        {
            return await context.Subjects.FindAsync(code);
        }

        public async Task<SubjectDbDTO> GetSubjectAsync(string name)
        {
            return await context.Subjects.FirstOrDefaultAsync(s => s.Name == name);
        }

        private IQueryable<SubjectDbDTO> OrderSubjects(string orderBy, bool orderAsc, IQueryable<SubjectDbDTO> subjects)
        {
            switch (orderBy)
            {
                case "population":
                    subjects = SubjectsOrderByPopulation(orderAsc, subjects);
                    break;
                case "square":
                    subjects = SubjectsOrderBySquare(orderAsc, subjects);
                    break;
                case "populationDencity":
                    subjects = SubjectsOrderByPopulationDencity(orderAsc, subjects);
                    break;
            }
            return subjects;
        }

        private static IQueryable<SubjectDbDTO> SubjectsOrderByPopulation(bool orderAsc, IQueryable<SubjectDbDTO> subjects)
        {
            return SubjectsOrderBy(orderAsc, subjects, s => s.Population);
        }

        private static IQueryable<SubjectDbDTO> SubjectsOrderBySquare(bool orderAsc, IQueryable<SubjectDbDTO> subjects)
        {
            return SubjectsOrderBy(orderAsc, subjects, s => s.Square);
        }

        private static IQueryable<SubjectDbDTO> SubjectsOrderByPopulationDencity(bool orderAsc, IQueryable<SubjectDbDTO> subjects)
        {
            return SubjectsOrderBy(orderAsc, subjects, s => s.Population / s.Square);
        }

        private static IQueryable<SubjectDbDTO> SubjectsOrderBy<TKey>(bool orderAsc, IQueryable<SubjectDbDTO> subjects, Expression<Func<SubjectDbDTO, TKey>> expression)
        {
            if (orderAsc) return subjects.OrderBy(expression);
            else return subjects.OrderByDescending(expression);
        }

        public void Create(SubjectDbDTO subject)
        {
            context.Add(subject);
        }

        public void Update(SubjectDbDTO subject)
        {
            context.Update(subject);
        }

        public void Delete(SubjectDbDTO subject)
        {
            context.Remove(subject);
        }

        public async Task<bool> SubjectExists(uint code)
        {
            return await context.Subjects.AnyAsync(s => s.Code == code);
        }
    }
}
