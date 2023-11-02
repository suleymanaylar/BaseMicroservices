using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IDiscountService _discountService;

        public DiscountsController(ISharedIdentityService identityService, IDiscountService discountService)
        {
            _identityService = identityService;
            _discountService = discountService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountService.GetById(id);
            return CreateActionResultInstance(discount);
        }
        [HttpGet]
        [Route("/api/[controller]/GetByCode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId =  _identityService.GetUserId; 
            var discount= await _discountService.GetByCodeAndUserId(userId, code);

            return CreateActionResultInstance(discount);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Entities.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Save(discount));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Entities.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Update(discount));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _discountService.DeleteById(id));
        }
    }
}
