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
    public class DistrictsService
    {
        public DistrictRepository districtRepository;

        public DistrictsService(DistrictRepository districtRepository)
        {
            this.districtRepository = districtRepository;
        }

        public async Task<IEnumerable<DistrictApiDTO>> GetAllDistrictsAsync(bool orderAsc, string? orderBy)
        {
            var districts = await districtRepository.GetAllDistrictsAsync(orderAsc, orderBy);
            return districts.Select(d => new DistrictApiDTO(d));
        }

        public async Task<(DistrictApiDTO, Exception)> GetDistrictAsync(uint code)
        {
            var district = await districtRepository.GetDistrictAsync(code);
            if (district == null) return (null, new KeyNotFoundException());
            else return (new DistrictApiDTO(district), null);
        }

        public async Task<Exception> CreateAsync(DistrictApiDTO district)
        {
            if (await districtRepository.DistrictExists(district.Code)) 
                    return new AlreadyExistException();

            var districtToAdd = district.Create();
            districtRepository.Create(districtToAdd);
            try
            {
                await districtRepository.SaveAsync();
            }
            catch (Exception)
            {
                return new SaveChangesException();
            }
            return null;
        }

        public async Task<Exception> DeleteAsync(uint code)
        {
            var district = await districtRepository.GetDistrictAsync(code);
            if (district == null) return new KeyNotFoundException(string.Format("Округа с кодом {0} не найдено", code));

            return await DeleteAsync(district);
        }

        public async Task<Exception> DeleteAsync(DistrictDbDTO district)
        {
            if (!await districtRepository.DistrictExists(district.Code)) return new KeyNotFoundException();

            districtRepository.Delete(district);
            try
            {
                await districtRepository.SaveAsync();
            }
            catch (Exception)
            {
                return new SaveChangesException();
            }
            return null;
        }

        public async Task<Exception> UpdateAsync(DistrictApiDTO district)
        {
            if (!await districtRepository.DistrictExists(district.Code)) return new KeyNotFoundException();
            districtRepository.Update(district.Create());

            try
            {
                await districtRepository.SaveAsync();
            }
            catch (Exception)
            {
                return new SaveChangesException();
            }
            return null;
        }
    }
}
