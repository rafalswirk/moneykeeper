using MoneyKeeper.Budget.Core.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyKeeper.Budget.Core.DTO;
using MoneyKeeper.Budget.Core.Repositories;
using FakeItEasy;
using MoneyKeeper.Budget.Core.Entities;
using MoneyKeeper.Budget.Repositories;
using MoneyKeeper.Budget.Core.Services.GCloud;
using MoneyKeeper.Budget.Entities;
using System.Collections.ObjectModel;
using MoneyKeeper.Budget.Core.DAL.Repositories;

namespace MoneyKeeper.UnitTests.Budget.Tests
{
    public class TransactionsTestFixture
    {
        [Fact]
        public async Task Create_TransactionFor2024_CreatingTransactionInRepositoryFor2024Year()
        {
            var spreadsheetRepository = A.Fake<ISpreadsheetRepository>();
            A.CallTo(() => spreadsheetRepository.BrowseAsync()).Returns(Task.FromResult<IReadOnlyCollection<Spreadsheet>>(new List<Spreadsheet>() 
            {
                new Spreadsheet {Id = 1, SpreadsheetKey = "1", Year = 2023},
                new Spreadsheet {Id = 1, SpreadsheetKey = "2", Year = 2024},
            }));
            var budgetCategoryRepository = A.Fake<IBudgetCategoryRepository>();
            A.CallTo(() => budgetCategoryRepository.BrowseAsync()).Returns(Task.FromResult<IReadOnlyCollection<BudgetCategory>>(new ReadOnlyCollection<BudgetCategory>(new List<BudgetCategory>
            {
                new BudgetCategory
                {
                    Id = 1,
                    Category = "Restaurants",
                    Group = "Food"
                }
            })));
            var categoryMap = A.Fake<ICategorySpreadsheetMapRepository>();
            var googleDocsEditor = A.Fake<IGoogleDocsEditor>();

            var creator = new TransactionCreator(spreadsheetRepository, budgetCategoryRepository, new SheetToMonthInMemoryRepository(), categoryMap, googleDocsEditor);
            await creator.Create(new TransactionDto(new DateTime(2024, 4, 22), 1, 22.4));
        }
    }
}
