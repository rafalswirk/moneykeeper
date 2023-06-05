using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Repositories
{
    public interface ITaxMappingRepository
    {
        Task AddAsync(TaxIdMapping category);
        Task DeleteAsync(TaxIdMapping category);
        Task<IReadOnlyCollection<TaxIdMapping>> BrowseAsync();
        Task<TaxIdMapping> GetAsync(int id);
        Task UpdateCategoryAsync(TaxIdMapping category);

        Task<TaxIdMapping> FindByTaxIdAsync(string taxId);
    }
}
