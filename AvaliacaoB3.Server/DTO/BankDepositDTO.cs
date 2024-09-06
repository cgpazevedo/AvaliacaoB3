using System.Text.Json.Serialization;

namespace AvaliacaoB3.Server.DTO
{
    [Serializable]
    public class BankDepositDtoRequest
    {
        public double Deposit { get; set; }
        public int Month { get; set; }
    }

    [Serializable]
    public class BankDepositDtoResponse
    {
        public double FinalYield { get; set; }
        public double FinalTax { get; set; }
    }

}
