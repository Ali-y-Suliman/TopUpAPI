using System.ComponentModel.DataAnnotations;
using TopUpAPI.Models;

namespace TopUpAPI.Dto
{
    public class AddUsersTopUpBeneficiariesDto
    {

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public int TopUpBeneficiaryId { get; set; }
        public decimal TopUpAmount { get; set; }

    }

    public class GetUsersTopUpBeneficiariesDto
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public User? User { get; set; }

        public TopUpBeneficiary? TopUpBeneficiary { get; set; }

        public ICollection<Transactions>? Transactions { get; set; }
    }

    public class UpdateUsersTopUpBeneficiariesDto
    {
        public bool IsActive { get; set; }

    }
}