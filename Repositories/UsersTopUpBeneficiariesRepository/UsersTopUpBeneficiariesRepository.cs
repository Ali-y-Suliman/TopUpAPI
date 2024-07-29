using Microsoft.EntityFrameworkCore;
using TopUpAPI.Data;
using TopUpAPI.Dto;
using TopUpAPI.Models;

namespace TopUpAPI.Repositories.UsersTopUpBeneficiariesRepository
{
    public class UsersTopUpBeneficiariesRepository : IUsersTopUpBeneficiariesRepository
    {
        private readonly DataContext _context;

        public UsersTopUpBeneficiariesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsersTopUpBeneficiaries>> GetUsersTopUpBeneficiaries(int userId)
        {
            return await _context.UsersTopUpBeneficiaries.Where(utb => utb.UserId == userId).ToListAsync();
        }

        public async Task<UsersTopUpBeneficiaries> AddUsersTopUpBeneficiaries(UsersTopUpBeneficiaries usersTopUpBeneficiaries)
        {
            _context.UsersTopUpBeneficiaries.Add(usersTopUpBeneficiaries);
            var id = await _context.SaveChangesAsync();
            var newUsersTopUpBeneficiaries = await _context.UsersTopUpBeneficiaries.FirstOrDefaultAsync(t => t.Id == id);
            if(newUsersTopUpBeneficiaries != null){
                return newUsersTopUpBeneficiaries;
            } else {
                throw new Exception("Failed to add UsersTopUpBeneficiary");
            }
        }

        public async Task<UsersTopUpBeneficiaries> UpdateUsersTopUpBeneficiariesAsync(UpdateUsersTopUpBeneficiariesDto usersTopUpBeneficiaries, int id)
        {
            var usersTopUpBeneficiary = await _context.UsersTopUpBeneficiaries.FindAsync(id);
            if (usersTopUpBeneficiary == null)
            {
                throw new Exception("UsersTopUpBeneficiary not found");
            }

            usersTopUpBeneficiary.IsActive = usersTopUpBeneficiaries.IsActive;

            await _context.SaveChangesAsync();

            return usersTopUpBeneficiary;
        }

    }
}
