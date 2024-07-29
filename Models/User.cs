using System.ComponentModel.DataAnnotations;

namespace TopUpAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public bool IsVerified { get; set; }
        
        public ICollection<UsersTopUpBeneficiaries>? UsersTopUpBeneficiaries { get; set; }
    }
}
