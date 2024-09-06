using AvaliacaoB3.Server.Domain;
using AvaliacaoB3.Server.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AvaliacaoB3.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterestController : ControllerBase
    {
        public InterestController()
        {
        }

        [HttpPost(Name = "PostInterest")]
        public BankDepositDtoResponse Post(BankDepositDtoRequest request)
        {
            var bdc = new BankDepositCertificate(request.Deposit, request.Month);

            var finalYield = bdc.ProcessDeposit();

            var finalTax = bdc.ProcessTax(finalYield);

            var result = new BankDepositDtoResponse()
            {
                FinalYield = finalYield,
                FinalTax = finalTax
            };

            return result;
        }

    }
}
