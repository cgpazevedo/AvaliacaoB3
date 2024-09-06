using AvaliacaoB3.Server.Domain.BusinessException;

namespace AvaliacaoB3.Server.Domain
{
    public class BankDepositCertificate
    {

        public BankDepositCertificate(double deposit, int month)
        {
            this.Deposit = deposit;
            this.Month = month;
        }

        private const string DepositMinValueValidation = "Code 001: Deposit value cannot be 0 or less";
        private const string MonthMinValueValidation = "Code 002: Month value cannot be 0 or less";
        private const string TaxRateNotFoundValidation = "Code 003: Tax rate not found for the months informed";

        private const double C_TB = 1.08;
        private const double C_CDI = 0.009;

        private static readonly (int montlyLimits, double taxRate)[] TaxTable = [
            (6, 0.225),  // Até 6 meses
            (12, 0.20),  // Até 12 meses
            (24, 0.175), // Até 24 meses
            (int.MaxValue, 0.15) // Acima de 24 meses
        ];
        
        private double _deposit;
        public double Deposit
        {
            get { return _deposit; }
            set
            {
                if (value <= 0)
                    throw new ValidationException(DepositMinValueValidation);
                
                _deposit = value;
            }
        }

        private int _month;
        public int Month
        {
            get { return _month; }
            set
            {
                if (value <= 0)
                    throw new ValidationException(MonthMinValueValidation);

                _month = value;
            }
        }


        public double ProcessDeposit()
        {
            double finalValue = this.Deposit;

            for (int i = 0; i < this.Month; i++)
            {
                finalValue *= (1 + C_CDI * C_TB);
            }

            return finalValue;
        }

        public double ProcessTax(double finalValue)
        {
            double yield = finalValue - this.Deposit;
            double taxRate = RetrieveTaxRate();

            return yield * taxRate;
        }

        public double RetrieveTaxRate()
        {
            foreach (var (monthlyLimit, taxRate) in TaxTable)
            {
                if (this.Month <= monthlyLimit)
                {
                    return taxRate;
                }
            }
            throw new ValidationException(TaxRateNotFoundValidation);
        }
    }
}
