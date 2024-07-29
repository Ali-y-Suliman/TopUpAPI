
using System.ComponentModel.DataAnnotations;
using TopUpAPI.Models;

namespace TopUpAPI.Dto
{
    public class AddTopUpBeneficiaryDto
    {
        [Required]
        [MaxLength(20)]
        public string Nickname { get; set; } = string.Empty;

    }

    public class GetTopUpBeneficiaryDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public ICollection<UsersTopUpBeneficiaries>? UsersTopUpBeneficiaries { get; set; }
    }
}