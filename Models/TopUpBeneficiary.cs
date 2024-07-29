using System.ComponentModel.DataAnnotations;

namespace TopUpAPI.Models
{
    public class TopUpBeneficiary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nickname { get; set; } = string.Empty;

        public ICollection<UsersTopUpBeneficiaries>? UsersTopUpBeneficiaries { get; set; }
    }
}
