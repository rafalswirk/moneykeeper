﻿using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Budget.Core.DTO;
using MoneyKeeper.Budget.Repositories;

namespace MoneyKeeper.Budget.API.Controllers
{
    [ApiController]
    [Route("api/receipt/companies")]
    public class CompaniesController : ControllerBase 
    {
        private readonly ITaxIdRepository _taxIdRepository;
        private readonly ITaxMappingRepository _taxMappingRepository;
        private readonly IBudgetCategoryRepository _budgetCategoryRepository;

        public CompaniesController(ITaxIdRepository taxIdRepository, ITaxMappingRepository taxMappingRepository, IBudgetCategoryRepository budgetCategoryRepository)
        {
            _taxIdRepository = taxIdRepository;
            _taxMappingRepository = taxMappingRepository;
            _budgetCategoryRepository = budgetCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompany(string taxId)
        {
            var company = await _taxIdRepository.FindByTaxIdAsync(taxId);
            var mapping = await _taxMappingRepository.FindByTaxIdAsync(taxId);
            if(company == null)
                return NotFound();
            return Ok(new CompanyDto(company.Id, company.TaxIdentificationNumber, company.CompanyName, mapping.Category.Id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyDto companyDto)
        {
            var companyId = await _taxIdRepository.AddAsync(new Entities.TaxId 
            {
                CompanyName = companyDto.Name,
                TaxIdentificationNumber = companyDto.TaxID
            });
            await _taxMappingRepository.AddAsync(new Entities.TaxIdMapping 
            {
                Category = await _budgetCategoryRepository.GetAsync(companyDto.CategoryId),
                TaxId = await _taxIdRepository.GetAsync(companyId)
            });

            return Ok();
        }

    }
}
