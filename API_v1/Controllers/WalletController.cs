using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService) {
            _walletService = walletService;
        }

        [HttpGet]
        public IActionResult Get() {
            _walletService.Topup(1, new Service.ThirdParty.Zalopay.OrderRequest());
            return Ok();
        }
    }
}
