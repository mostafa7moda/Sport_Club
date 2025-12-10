using Microsoft.EntityFrameworkCore;
using Sport_Club.Data;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_Club.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await _context.Subscriptions
                .Include(s => s.MemberSection)
                .ToListAsync();
        }

        public async Task<Subscription?> GetByIdAsync(int id)
        {
            return await _context.Subscriptions
                .Include(s => s.MemberSection)
                .FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task<IEnumerable<Subscription>> GetByMemberSectionIdAsync(int memberSectionId)
        {
             return await _context.Subscriptions
                .Where(s => s.MemberSectionId == memberSectionId)
                .ToListAsync();
        }

        public async Task AddAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
        }

        public void Update(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
        }

        public void Delete(Subscription subscription)
        {
            _context.Subscriptions.Remove(subscription);
        }
    }
}
