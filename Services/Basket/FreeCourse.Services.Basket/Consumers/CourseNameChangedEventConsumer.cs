using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared.Messages;
using FreeCourse.Shared.Services;
using MassTransit;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly RedisService _redisService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CourseNameChangedEventConsumer(RedisService redisService, ISharedIdentityService sharedIdentityService)
        {
            _redisService = redisService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(_sharedIdentityService.GetUserId);

            //var baskets = JsonSerializer.Deserialize<BasketDto>(existBasket);
            //var basketsItem = baskets.basketItems.Where(t => t.CourseId == context.Message.CourseId);
            //basketsItem.CourseName ==

             throw new System.NotImplementedException();
        }
    }
}
