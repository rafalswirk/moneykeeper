﻿using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Budget.Core.DTO;
using MoneyKeeper.Budget.Core.Repositories;
using MoneyKeeper.Budget.Core.Services.GCloud;
using MoneyKeeper.Budget.Core.Services.Transactions;
using MoneyKeeper.Budget.Repositories;

namespace MoneyKeeper.Budget.API.Controllers
{
    [ApiController]
    [Route("api/budget")]
    public class BudgetController : ControllerBase
    {
        private readonly DayToColumnCalculator _dayToColumn = new DayToColumnCalculator();

        private readonly IBudgetCategoryRepository _repository;
        private readonly ITaxIdRepository _taxIdRepository;
        private readonly ITaxMappingRepository _taxMappingRepository;
        private readonly ICategorySpreadsheetMapRepository _categorySpreadsheetMapRepository;
        private readonly ISheetToMonthMapRepository _sheetToMonthMapRepository;
        private readonly IGoogleDocsEditor _googleDocsEditor;
        private readonly IBudgetCategoryRepository _budgetCategoryRepository;
        private readonly ISpreadsheetRepository _spreadsheetRepository;
        private readonly ITransactionCreator _transactionCreator;

        public BudgetController(IBudgetCategoryRepository repository,
                                ITaxIdRepository taxIdRepository,
                                ITaxMappingRepository taxMappingRepository,
                                ICategorySpreadsheetMapRepository categorySpreadsheetMapRepository,
                                ISheetToMonthMapRepository sheetToMonthMapRepository,
                                IGoogleDocsEditor googleDocsEditor,
                                IBudgetCategoryRepository budgetCategoryRepository,
                                ISpreadsheetRepository spreadsheetRepository,
                                ITransactionCreator transactionCreator)
        {
            _repository = repository;
            _taxIdRepository = taxIdRepository;
            _taxMappingRepository = taxMappingRepository;
            _categorySpreadsheetMapRepository = categorySpreadsheetMapRepository;
            _sheetToMonthMapRepository = sheetToMonthMapRepository;
            _googleDocsEditor = googleDocsEditor;
            _budgetCategoryRepository = budgetCategoryRepository;
            _spreadsheetRepository = spreadsheetRepository;
            _transactionCreator = transactionCreator;
        }

        [HttpPost]
        public async Task<IActionResult> SendData([FromBody] ReceiptDataDto dto)
        {
            try
            {
                var budgetCategories = await _budgetCategoryRepository.BrowseAsync();
                var taxIds = await _taxIdRepository.BrowseAsync();
                var company = taxIds.Single(c => c.TaxIdentificationNumber == dto.TaxId);
                var mappings = await _taxMappingRepository.BrowseAsync();
                var category = mappings.Single(m => m.TaxId.Id == company.Id).Category;
                var sheetToMonth = await _sheetToMonthMapRepository.BrowseAsync();

                var spreadsheetMap = await _categorySpreadsheetMapRepository.BrowseAsync();
                var row = spreadsheetMap.Single(m => m.Category.Id == category.Id).Row;
                var spreadsheet = await _spreadsheetRepository.GetSpreadsheetByYear(dto.TransactionTime.Year);

                await _googleDocsEditor.AddValueToGoogleDocsAsync(
                    spreadsheet.SpreadsheetKey,
                    sheetToMonth.Single(s => s.Month == dto.TransactionTime.Month).SheetName,
                    row,
                    _dayToColumn.CalculateColumn(dto.TransactionTime.Day),
                    dto.Total.ToString());
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDto dto)
        {
            await _transactionCreator.Create(dto);
            return Ok();
        }
    }
}
