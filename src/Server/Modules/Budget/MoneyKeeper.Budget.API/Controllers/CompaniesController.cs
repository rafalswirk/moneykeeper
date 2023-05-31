using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Budget.Core.DTO;
using MoneyKeeper.Budget.Repositories;

namespace MoneyKeeper.Budget.API.Controllers
{
    [ApiController]
    [Route("api/receipt/companies")]
    public class CompaniesController : ControllerBase 
    {
        private readonly ITaxIdRepository _taxIdRepository;

        public CompaniesController(ITaxIdRepository taxIdRepository)
        {
            _taxIdRepository = taxIdRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompany(string taxId)
        {
            var company = await _taxIdRepository.FindByTaxIdAsync(taxId);
            if(company == null)
                return NotFound();
            return Ok(new CompanyDto(company.Id, company.TaxIdentificationNumber, company.CompanyName));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyDto companyDto)
        {
            await _taxIdRepository.AddAsync(new Entities.TaxId 
            {
                CompanyName = companyDto.Name,
                TaxIdentificationNumber = companyDto.TaxID
            });

            return Ok();
        }
    }
}
