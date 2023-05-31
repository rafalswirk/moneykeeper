using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Budget.Entities;
using MoneyKeeper.Budget.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.DAL.Repositories
{
    public class TaxIdRepository : ITaxIdRepository
    {
        private readonly BudgetCategoryDbContext _context;

        public TaxIdRepository(BudgetCategoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaxId taxId)
        {
            _context.TaxIds.Add(taxId);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<TaxId>> BrowseAsync()
            => await _context.TaxIds.ToListAsync();

        public async Task DeleteAsync(TaxId taxId)
        {
            _context.TaxIds.Remove(taxId);
            await _context.SaveChangesAsync();
        }

        public async Task<TaxId> GetAsync(int id)
            => await _context.TaxIds.SingleAsync(t => t.Id == id);

        public async Task UpdateAsync(TaxId taxId)
        {
            _context.TaxIds.Update(taxId);
            await _context.SaveChangesAsync();
        }

        public async Task<TaxId> FindByTaxIdAsync(string taxIdNumber)
            => await _context.TaxIds.SingleOrDefaultAsync(t => t.TaxIdentificationNumber == taxIdNumber);
        
    }
}
