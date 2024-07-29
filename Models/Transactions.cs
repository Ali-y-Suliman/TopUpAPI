using System.ComponentModel.DataAnnotations;

namespace TopUpAPI.Models
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal TopUpAmount { get; set; }

        [Required]
        public decimal TopUpFeeAmount { get; set; }

        [Required]
        public decimal TopUpTotalAmount { get; set; }

        [Required]
        public int UsersTopUpBeneficiariesId { get; set; }
        public UsersTopUpBeneficiaries? UsersTopUpBeneficiaries { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }
    }
}
