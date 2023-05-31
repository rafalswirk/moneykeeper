using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Repositories
{
    public interface ITaxIdRepository
    {
        Task AddAsync(TaxId taxId);
        Task DeleteAsync(TaxId taxId);
        Task<IReadOnlyCollection<TaxId>> BrowseAsync();
        Task<TaxId> GetAsync(int id);
        Task UpdateAsync(TaxId taxId);
        Task<TaxId> FindByTaxIdAsync(string taxIdNumber);
    }
}
