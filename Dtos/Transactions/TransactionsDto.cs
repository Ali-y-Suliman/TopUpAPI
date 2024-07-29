using TopUpAPI.Models;

namespace TopUpAPI.Dto
{
    public class AddTransactionDto
    {
        public decimal TopUpAmount { get; set; }

        public int UsersTopUpBeneficiariesId { get; set; }
        public decimal TopUpFeeAmount { get; set; }
        public decimal TopUpTotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }

    }

    public class GetTransactionDto
    {
        public int Id { get; set; }

        public decimal TopUpAmount { get; set; }
        public decimal TopUpFeeAmount { get; set; }
        public decimal TopUpTotalAmount { get; set; }

        public UsersTopUpBeneficiaries? UsersTopUpBeneficiaries { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}