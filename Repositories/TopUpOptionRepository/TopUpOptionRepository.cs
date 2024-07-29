using Microsoft.EntityFrameworkCore;
using TopUpAPI.Data;
using TopUpAPI.Models;

namespace TopUpAPI.Repositories.TopUpOptionRepository
{
    public class TopUpOptionRepository : ITopUpOptionRepository
    {
        private readonly DataContext _context;

        public TopUpOptionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TopUpOption>> GetTopUpOptions()
        {
            return await _context.TopUpOptions.ToListAsync();
        }

        public async Task<TopUpOption> AddTopUpOption(TopUpOption topUpOption)
        {
            _context.TopUpOptions.Add(topUpOption);
            var id = await _context.SaveChangesAsync();
            var newTopUpOption = await _context.TopUpOptions.FirstOrDefaultAsync(t => t.Id == id);
            if(newTopUpOption != null){
                return newTopUpOption;
            } else {
                throw new Exception("Failed to add topUpOption");
            }
        }
    }
}
