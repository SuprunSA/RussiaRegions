using SubjectsAndDistrictsDbContext.Model.DTO;
using SubjectsAndDistrictsDbContext.Repositories;
using SubjectsAndDistrictsWebApi.BL.Exceptions;
using SubjectsAndDistrictsWebApi.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Services
{
    public class SubjectsService
    {
        public SubjectRepository subjectRepository;

        public SubjectsService(SubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<SubjectApiDTO>> GetAllSubjectsAsync(string? filter, bool orderAsc, string? orderBy, bool order)
        {
            var subjects = await subjectRepository.GetAllSubjectsAsync(filter, orderAsc, orderBy, order);
            return subjects.Select(s => new SubjectApiDTO(s));
        }

        public async Task<IEnumerable<SubjectApiDTO>> GetFilteredSubjects(string name, string? orderBy, bool orderAsc, bool order) 
        {
            var subjects = await subjectRepository.FilterSubjectsAsync(name, orderBy, orderAsc, order);
            return subjects.Select(s => new SubjectApiDTO(s));
        }

        public async Task<SubjectApiDTO> GetSubjectAsync(string name)
        {
            var subject = await subjectRepository.GetSubjectAsync(name);
            if (subject == null) return null;
            else return new SubjectApiDTO(subject);
        }

        public async Task<SubjectApiDTO> GetSubjectAsync(uint code)
        {
            var subject = await subjectRepository.GetSubjectAsync(code);
            if (subject == null) return null;
            else return new SubjectApiDTO(subject);
        }

        public async Task<Exception> CreateAsync(SubjectApiDTO subject)
        {
            if (await subjectRepository.SubjectExists(subject.Code))
                    return new AlreadyExistException();

            var subjectToAdd = subject.Create();
            subjectRepository.Create(subjectToAdd);
            try
            {
                await subjectRepository.SaveAsync();
            }
            catch (Exception)
            {
                return new SaveChangesException();
            }
            return null;
        }

        public async Task<Exception> DeleteAsync(uint code)
        {
            var subject = await subjectRepository.GetSubjectAsync(code);
            if (subject == null) return new KeyNotFoundException();
            subjectRepository.Delete(subject);
            try
            {
                await subjectRepository.SaveAsync();
            }
            catch (Exception)
            {
                return new SaveChangesException();
            }
            return null;
        }

        public async Task<Exception> DeleteAsync(SubjectDbDTO subject)
        {
            if (!await subjectRepository.SubjectExists(subject.Code)) return new KeyNotFoundException();

            subjectRepository.Delete(subject);
            try
            {
                await subjectRepository.SaveAsync();
            }
            catch (Exception)
            {
                return new SaveChangesException();
            }
            return null;
        }

        public async Task<Exception> UpdateAsync(SubjectApiDTO subject)
        {
            if (!await subjectRepository.SubjectExists(subject.Code)) return new KeyNotFoundException();
            subjectRepository.Update(subject.Create());

            try
            {
                await subjectRepository.SaveAsync();
            }
            catch (Exception)
            {
                return new SaveChangesException();
            }
            return null;
        }
    }
}
