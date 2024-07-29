using System.ComponentModel.DataAnnotations;

namespace TopUpAPI.Models
{
    public class UsersTopUpBeneficiaries
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int UserId { get; set; }

        public User? User { get; set; }

        [Required]
        public int TopUpBeneficiaryId { get; set; }

        public TopUpBeneficiary? TopUpBeneficiary { get; set; }

        public ICollection<Transactions>? Transactions { get; set; }
    }
}
