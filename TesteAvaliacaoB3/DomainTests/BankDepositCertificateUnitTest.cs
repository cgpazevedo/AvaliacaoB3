using AvaliacaoB3.Server.Domain;
using AvaliacaoB3.Server.Domain.BusinessException;

namespace TesteAvaliacaoB3.DomainTests
{
    [TestClass]
    public class BankDepositCertificateUnitTest
    {
        [TestMethod]
        public void constructor_must_create_bank_deposit_certificate_correctly()
        {
            var month = 2;
            var deposit = 100.0;

            var bdc = new BankDepositCertificate(deposit, month);

            Assert.IsNotNull(bdc);

        }

        [TestMethod]
        public void bank_deposit_must_be_bigger_than_zero()
        {
            var month = 2;
            var deposit = 2.0;

            var bdc = new BankDepositCertificate(deposit, month);

            Assert.IsTrue(bdc.Deposit >= 0);

        }

        [TestMethod]
        public void bank_deposit_must_throw_exception_if_zero_or_less()
        {
            var month = 2;

            Assert.ThrowsException<ValidationException>(() => new BankDepositCertificate(0, month));

            Assert.ThrowsException<ValidationException>(() => new BankDepositCertificate(-19, month));
        }

        [TestMethod]
        public void bank_months_must_be_bigger_than_zero()
        {
            var month = 2;
            var deposit = 100.0;

            var bdc = new BankDepositCertificate(deposit, month);

            Assert.IsTrue(bdc.Month > 0);

        }

        [TestMethod]
        public void bank_month_must_throw_exception_if_zero_or_less()
        {
            var deposit = 100.0;

            Assert.ThrowsException<ValidationException>(() => new BankDepositCertificate(deposit, 0));

            Assert.ThrowsException<ValidationException>(() => new BankDepositCertificate(deposit, -4));
        }

        [TestMethod]
        public void must_process_yield_correctly()
        {
            var deposit = 100.0;
            var months = 4;

            var finalValue = 103.94;

            var bankDepositCertificate = new BankDepositCertificate(deposit, months);

            var finalProcessedYield = bankDepositCertificate.ProcessDeposit();

            // Define the tolerance for variation in their values
            double difference = Math.Abs(finalValue * .0001f);

            // Compare the values
            // The output to the console indicates that the two values are equal
            Assert.IsTrue(Math.Abs(finalValue - finalProcessedYield) <= difference);
        }


        [TestMethod]
        public void must_process_tax_correctly()
        {
            var deposit = 100.0;
            var months = 4;

            var expectedFinalTax = 0.887;

            var bankDepositCertificate = new BankDepositCertificate(deposit, months);

            var finalProcessedYield = bankDepositCertificate.ProcessDeposit();

            var finalProcessedTax = bankDepositCertificate.ProcessTax(finalProcessedYield);

            // Define the tolerance for variation in their values
            double difference = Math.Abs(expectedFinalTax * .0001f);

            // Compare the values
            // The output to the console indicates that the two values are equal
            Assert.IsTrue(Math.Abs(expectedFinalTax - finalProcessedTax) <= difference);
        }

        [TestMethod]
        public void must_return_correct_tax_rate()
        {
            var deposit = 100.0;
            var months = 10;

            var expectedTaxRate = 0.20;

            var bankDepositCertificate = new BankDepositCertificate(deposit, months);

            var finalTaxRate = bankDepositCertificate.RetrieveTaxRate();

            // Define the tolerance for variation in their values
            double difference = Math.Abs(expectedTaxRate * .0001f);

            // Compare the values
            // The output to the console indicates that the two values are equal
            Assert.IsTrue(Math.Abs(expectedTaxRate - finalTaxRate) <= difference);
        }
    }
}