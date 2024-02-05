using MoneyKeeper.Budget.DAL.Repositories;
using MoneyKeeper.Budget.Entities;
using MoneyKeeper.Budget.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.DAL.Repositories
{
    public class SheetToMonthInMemoryRepository : ISheetToMonthMapRepository
    {
        private readonly IReadOnlyCollection<SheetToMonthMap> _sheetsToMonths;

        public SheetToMonthInMemoryRepository()
        {
            _sheetsToMonths = new ReadOnlyCollection<SheetToMonthMap>(new List<SheetToMonthMap> 
            {
                new SheetToMonthMap { Id = 1, Month = 1, SheetName = "Styczeń", },
                new SheetToMonthMap { Id = 2, Month = 2, SheetName = "Luty", },
                new SheetToMonthMap { Id = 3, Month = 3, SheetName = "Marzec", },
                new SheetToMonthMap { Id = 4, Month = 4, SheetName = "Kwiecień", },
                new SheetToMonthMap { Id = 5, Month = 5, SheetName = "Maj", },
                new SheetToMonthMap { Id = 6, Month = 6, SheetName = "Czerwiec", },
                new SheetToMonthMap { Id = 7, Month = 7, SheetName = "Lipiec", },
                new SheetToMonthMap { Id = 8, Month = 8, SheetName = "Sierpień", },
                new SheetToMonthMap { Id = 9, Month = 9, SheetName = "Wrzesień", },
                new SheetToMonthMap { Id = 10, Month = 10, SheetName = "Październik", },
                new SheetToMonthMap { Id = 11, Month = 11, SheetName = "Listopad", },
                new SheetToMonthMap { Id = 12, Month = 12, SheetName = "Grudzień", },
            });    
        }

        public Task AddAsync(SheetToMonthMap map)
        {
            throw new InvalidOperationException("This repository already contains default values. It's not allowe to modify them");
        }

        public Task<IReadOnlyCollection<SheetToMonthMap>> BrowseAsync()
            => Task.FromResult(_sheetsToMonths);

        public Task DeleteAsync(SheetToMonthMap map)
        {
            throw new InvalidOperationException("This repository already contains default values. It's not allowe to modify them");
        }

        public Task<SheetToMonthMap> GetAsync(int id)
            => Task.FromResult(_sheetsToMonths.Single(s => s.Id == id));
        public Task UpdateAsync(SheetToMonthMap map)
        {
            throw new InvalidOperationException("This repository already contains default values. It's not allowe to modify them");
        }
    }
}
