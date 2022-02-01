using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubjectsAndDistrictsWebApi.BL.Exceptions;
using SubjectsAndDistrictsWebApi.BL.Model;
using SubjectsAndDistrictsWebApi.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        SubjectsService subjectService;

        public SubjectController(SubjectsService subjectService)
        {
            this.subjectService = subjectService;
        }

        [AllowAnonymous]
        [HttpGet("order")]
        public async Task<IEnumerable<SubjectApiDTO>> Get(string? filter, string? orderBy, bool orderAsc, bool order)
        {
            return await subjectService.GetAllSubjectsAsync(filter, orderAsc, orderBy, order);
        }

        [AllowAnonymous]
        [HttpGet("filterDistrict")]
        public async Task<IEnumerable<SubjectApiDTO>> GetFiltered(string name, string? orderBy, bool orderAsc, bool order)
        {
            return await subjectService.GetFilteredSubjects(name, orderBy, orderAsc, order);
        }

        [AllowAnonymous]
        [HttpGet("name")]
        public async Task<ActionResult<SubjectApiDTO>> GetByName(string name)
        {
            var subject = await subjectService.GetSubjectAsync(name);
            if (subject == null) return StatusCode(404, string.Format("Субъекта с именем {0} не найдено", name));
            else return Ok(subject);
        }

        [AllowAnonymous]
        [HttpGet("{code}")]
        public async Task<ActionResult<SubjectApiDTO>> GetByCode(uint code)
        {
            var subject = await subjectService.GetSubjectAsync(code);
            if (subject == null) return StatusCode(404, string.Format("Субъекта с кодом {0} не найдено", code));
            else return Ok(subject);
        }

        [HttpPut]
        public async Task<ActionResult<Exception>> Put([FromBody] SubjectApiDTO subject)
        {
            var ex = await subjectService.UpdateAsync(subject);
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, ex.Message);
                if (ex is SaveChangesException) return StatusCode(500, ex.Message);
                else return StatusCode(400, ex.Message);
            }
            else return StatusCode(200);
        }

        [HttpPost]
        public async Task<ActionResult<Exception>> Post([FromBody] SubjectApiDTO subject)
        {
            var ex = await subjectService.CreateAsync(subject);
            if (ex != null)
            {
                if (ex is AlreadyExistException) return StatusCode(409, ex.Message);
                if (ex is SaveChangesException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            else return StatusCode(200);
        }

        [HttpDelete]
        public async Task<ActionResult<Exception>> Delete([FromBody] SubjectApiDTO subject)
        {
            var ex = await subjectService.DeleteAsync(subject.Create());
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, ex.Message);
                if (ex is SaveChangesException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200);
        }

        [HttpDelete("{code}")]
        public async Task<ActionResult<Exception>> Delete(uint code)
        {
            var ex = await subjectService.DeleteAsync(code);
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, ex.Message);
                if (ex is SaveChangesException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200);
        }
    }
}
