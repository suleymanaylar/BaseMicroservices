using Dapper;
using FreeCourse.Services.Discount.Entities;
using FreeCourse.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> DeleteById(int id)
        {
            var status = await _connection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discound Not Found", 404);

        }

        public async Task<Response<List<Entities.Discount>>> GetAll()
        {
            var discounts = await _connection.QueryAsync<Entities.Discount>("select * from discount");
            return Response<List<Entities.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Entities.Discount>> GetByCodeAndUserId(string userId, string code)
        {
            var discounts = await _connection.QueryAsync<Entities.Discount>("select * from discount where userid=@UserId and code=@Code", new { UserId = userId, Code = code });
            var hasDiscount = discounts.FirstOrDefault();
            return hasDiscount == null ? Response<Entities.Discount>.Fail("Discound Not Found", 404) : Response<Entities.Discount>.Success(hasDiscount, 200);
        }

        public async Task<Response<Entities.Discount>> GetById(int id)
        {
            var discount = (await _connection.QueryAsync<Entities.Discount>("select * from discount where id=@Id", new { id })).SingleOrDefault();
            if (discount == null)
                return Response<Entities.Discount>.Fail("Discount not found", 404);

            return Response<Entities.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Entities.Discount discount)
        {
            var saveStatus = await _connection.ExecuteAsync("insert into discount(userid,rate,code) values(@UserId,@Rate,@Code)", discount);
            if (saveStatus > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("an error accured while adding", 500);
        }

        public async Task<Response<NoContent>> Update(Entities.Discount discount)
        {
            var status = await _connection.ExecuteAsync("update discount set userid=@UserId,rate=@Rate,code=@code where id=@Id", new { id = discount.Id, UserId = discount.UserId, rate = discount.Rate, code = discount.Code });

            if (status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("Discound Not Found", 404);
        }
    }
}
