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
    public class TaxIdMappingRepository : ITaxMappingRepository
    {
        private readonly BudgetCategoryDbContext _context;

        public TaxIdMappingRepository(BudgetCategoryDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TaxIdMapping category)
        {
            await _context.TaxIdMapping.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<TaxIdMapping>> BrowseAsync()
            => await _context.TaxIdMapping.ToListAsync();

        public async Task DeleteAsync(TaxIdMapping category)
        {
            _context.TaxIdMapping.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<TaxIdMapping> FindByTaxIdAsync(string taxId)
            => await _context.TaxIdMapping.SingleAsync(t => t.TaxId.TaxIdentificationNumber == taxId);

        public async Task<TaxIdMapping> GetAsync(int id)
            => await _context.TaxIdMapping.SingleAsync(x => x.Id == id);

        public async Task UpdateCategoryAsync(TaxIdMapping category)
        {
            _context.TaxIdMapping.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
