using Microsoft.EntityFrameworkCore;
using TopUpAPI.Data;
using TopUpAPI.Models;

namespace TopUpAPI.Repositories.TopUpBeneficiaryRepository
{
    public class TopUpBeneficiaryRepository : ITopUpBeneficiaryRepository
    {
        private readonly DataContext _context;

        public TopUpBeneficiaryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TopUpBeneficiary>> GetTopUpBeneficiaries()
        {
            return await _context.TopUpBeneficiaries.ToListAsync();
        }

        public async Task<TopUpBeneficiary> AddTopUpBeneficiary(TopUpBeneficiary topUpBeneficiary)
        {
            _context.TopUpBeneficiaries.Add(topUpBeneficiary);
            var id = await _context.SaveChangesAsync();
            var newTopUpBeneficiary = await _context.TopUpBeneficiaries.FirstOrDefaultAsync(t => t.Id == id);
            if(newTopUpBeneficiary != null){
                return newTopUpBeneficiary;
            } else {
                throw new Exception("Failed to add top up beneficiary");
            }
        }
    }
}
