﻿using MoneyKeeper.Budget.Core.Repositories;
using MoneyKeeper.Budget.Core.Services.GCloud;
using MoneyKeeper.Budget.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services.Transactions
{
    public class TransactionCreator : ITransactionCreator
    {
        private readonly DayToColumnCalculator _dayToColumn = new DayToColumnCalculator();
        private readonly ISpreadsheetRepository _spreadsheetRepository;
        private readonly IBudgetCategoryRepository _budgetCategoryRepository;
        private readonly ISheetToMonthMapRepository _sheetToMonthMapRepository;
        private readonly ICategorySpreadsheetMapRepository _categorySpreadsheetMapRepository;
        private readonly IGoogleDocsEditor _googleDocsEditor;

        public TransactionCreator(ISpreadsheetRepository spreadsheetRepository,
                                  IBudgetCategoryRepository budgetCategoryRepository,
                                  ISheetToMonthMapRepository sheetToMonthMapRepository,
                                  ICategorySpreadsheetMapRepository categorySpreadsheetMapRepository,
                                  IGoogleDocsEditor googleDocsEditor)
        {
            _spreadsheetRepository = spreadsheetRepository;
            _budgetCategoryRepository = budgetCategoryRepository;
            _sheetToMonthMapRepository = sheetToMonthMapRepository;
            _categorySpreadsheetMapRepository = categorySpreadsheetMapRepository;
            _googleDocsEditor = googleDocsEditor;
        }

        public async Task Create(DTO.TransactionDto dto)
        {
            try
            {
                var budgetCategories = await _budgetCategoryRepository.BrowseAsync();
                var sheetToMonth = await _sheetToMonthMapRepository.BrowseAsync();

                var spreadsheetMap = await _categorySpreadsheetMapRepository.BrowseAsync();
                var row = spreadsheetMap.Single(m => m.Category.Id == dto.CategoryId).Row;
                var spreadsheet = await _spreadsheetRepository.GetSpreadsheetByYear(dto.Date.Year);

                await _googleDocsEditor.AddValueToGoogleDocsAsync(
                    spreadsheet.SpreadsheetKey,
                    sheetToMonth.Single(s => s.Month == dto.Date.Month).SheetName,
                    row,
                    _dayToColumn.CalculateColumn(dto.Date.Day),
                    dto.Sum.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
