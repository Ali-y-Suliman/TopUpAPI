using TopUpAPI.Models;

namespace TopUpAPI.Dto
{
    public class AddTopUpOptionDto
    {
        public decimal Amount { get; set; }

    }

    public class GetTopUpOptionDto
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }
    }
}