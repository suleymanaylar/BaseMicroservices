using FreeCourse.Services.Discount.Entities;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Entities.Discount>>> GetAll();

        Task<Response<Entities.Discount>> GetById(int id);
        Task<Response<NoContent>> Save(Entities.Discount discount);
        Task<Response<NoContent>> Update(Entities.Discount discount);
        Task<Response<NoContent>> DeleteById(int id);

        Task<Response<Entities.Discount>> GetByCodeAndUserId(string userId, string code);

    }
}
