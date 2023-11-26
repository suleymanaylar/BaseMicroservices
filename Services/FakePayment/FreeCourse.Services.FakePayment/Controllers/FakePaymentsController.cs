using FreeCourse.Services.FakePayment.Models;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivedPayment(PaymentDto paymentDto)
        {
            //paymentDto ile ödeme işlemi gerçekleşir.
            var sendEndPoint = await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand();
            createOrderMessageCommand.BuyerId = paymentDto.Order.BuyerId;
            createOrderMessageCommand.Provice = paymentDto.Order.Address.Provice;
            createOrderMessageCommand.District=paymentDto.Order.Address.District;
            createOrderMessageCommand.Line = paymentDto.Order.Address.Line;
            createOrderMessageCommand.ZipCode = paymentDto.Order.Address.ZipCode;
            createOrderMessageCommand.Street = paymentDto.Order.Address.Street;

            paymentDto.Order.OrderItems.ForEach(x =>
            {
                createOrderMessageCommand.OrderItems.Add(new OrderItem
                {
                    PictureUrl = x.PictureUrl,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                });
            });

            await sendEndPoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

            return CreateActionResultInstance<NoContent>(Shared.Dtos.Response<NoContent>.Success(200));
        }
    }
}
