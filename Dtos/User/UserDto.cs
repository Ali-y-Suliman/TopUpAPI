using System.ComponentModel.DataAnnotations;
using TopUpAPI.Models;

namespace TopUpAPI.Dto
{
    public class AddUserDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
    }

    public class GetUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public ICollection<UsersTopUpBeneficiaries>? UsersTopUpBeneficiaries { get; set; }
    }
}