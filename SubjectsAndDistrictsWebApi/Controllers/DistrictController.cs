﻿using Microsoft.AspNetCore.Authorization;
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
    public class DistrictController : ControllerBase
    {
        DistrictsService districtService;

        public DistrictController(DistrictsService districtService)
        {
            this.districtService = districtService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<DistrictApiDTO>> Get(string? orderBy, bool orderAsc, bool order)
        {
            return await districtService.GetAllDistrictsAsync(orderAsc, orderBy, order);
        }

        [AllowAnonymous]
        [HttpGet("name/{name}")]
        public async Task<ActionResult<DistrictApiDTO>> GetByName(string name)
        {
            (var district, var ex) = await districtService.GetDistrictAsync(name);
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, string.Format("Округа с именем {0} не найдено", name));
                else return StatusCode(500, ex);
            }
            return StatusCode(200, district);
        }

        [AllowAnonymous]
        [HttpGet("{code}")]
        public async Task<ActionResult<DistrictApiDTO>> GetByCode(uint code)
        {
            (var district, var ex) = await districtService.GetDistrictAsync(code);
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, string.Format("Округа с кодом {0} не найдено", code));
                else return StatusCode(500, ex);
            }
            return StatusCode(200, district);
        }

        [HttpPut]
        public async Task<ActionResult> Put(uint code, string name)
        {
            var ex = await districtService.UpdateAsync(code , name);
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, string.Format("Округ с кодом {0} не найден", code));
                if (ex is SaveChangesException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DistrictApiDTO district)
        {
            var ex = await districtService.CreateAsync(district);
            if (ex != null)
            {
                if (ex is AlreadyExistException) return StatusCode(409, ex.Message);
                if (ex is SaveChangesException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DistrictApiDTO district)
        {
            var ex = await districtService.DeleteAsync(district.Create());
            if (ex != null)
            {
                if (ex is KeyNotFoundException) return StatusCode(404, ex.Message);
                if (ex is SaveChangesException) return StatusCode(500, ex.Message);
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200);
        }

        [HttpDelete("{code}")]
        public async Task<ActionResult> Delete(uint code)
        {
            var ex = await districtService.DeleteAsync(code);
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
