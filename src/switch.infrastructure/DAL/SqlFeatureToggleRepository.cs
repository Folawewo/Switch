using Microsoft.EntityFrameworkCore;
using @switch.domain.Entities;
using @switch.domain.Repository;

namespace @switch.infrastructure.DAL
{
    public class SqlFeatureToggleRepository : IRepository<SwitchToggle>
    {
        private readonly SwitchDbContext _context;

        public SqlFeatureToggleRepository(SwitchDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SwitchToggle>> GetAllAsync()
        {
            return await _context.SwitchToggles.ToListAsync();
        }

        public async Task<SwitchToggle?> GetByIdAsync(Guid id)
        {
            return await _context.SwitchToggles.FindAsync(id);
        }

        public async Task AddAsync(SwitchToggle entity)
        {
            await _context.SwitchToggles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SwitchToggle entity)
        {
            _context.SwitchToggles.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.SwitchToggles.FindAsync(id);
            if (entity != null)
            {
                _context.SwitchToggles.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}