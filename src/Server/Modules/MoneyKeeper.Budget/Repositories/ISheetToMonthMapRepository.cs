using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Repositories
{
    public interface ISheetToMonthMapRepository
    {
        Task AddAsync(SheetToMonthMap map);
        Task DeleteAsync(SheetToMonthMap map);
        Task<IReadOnlyCollection<SheetToMonthMap>> BrowseAsync();
        Task<SheetToMonthMap> GetAsync(int id);
        Task UpdateAsync(SheetToMonthMap map);
    }
}
