using CompanyApi.Entities;
using CompanyApi.Models;
using CompanyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CompanyApi.Controllers
{
    [Authorize]
    [Route("company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyDto>> GetAll()
        {
            var companies = _companyService.GetAll();

            return Ok(companies);
        }


        [HttpGet("{id}")]
        public ActionResult<Company> GetById([FromRoute]long id)
        {
            var company = _companyService.GetById(id);

            return Ok(company);
        }


        [HttpPost("create")]
        public ActionResult CreateCompany([FromBody]CreateCompanyDto dto)
        {
            var id = _companyService.Create(dto);

            return Created($"company/{id}", id);
        }


        [AllowAnonymous]
        [HttpPost("search")]
        public ActionResult<IEnumerable<CompanyDto>> SearchCompanies([FromBody]SearchCompanyDto dto)
        {
            var companies = _companyService.Search(dto);

            return Ok(companies);
        }


        [HttpPut("update/{id}")]
        public ActionResult UpdateCompany([FromBody]CreateCompanyDto dto, [FromRoute]long id)
        {
            _companyService.Update(id, dto);

            return Ok();
        }


        [HttpDelete("delete/{id}")]
        public ActionResult DeleteCompany([FromRoute]long id)
        {
            _companyService.Delete(id);

            return Ok();
        }

    }
}
