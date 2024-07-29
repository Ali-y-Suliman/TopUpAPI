using System.ComponentModel.DataAnnotations;

namespace TopUpAPI.Models
{
    public class TopUpOption
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
