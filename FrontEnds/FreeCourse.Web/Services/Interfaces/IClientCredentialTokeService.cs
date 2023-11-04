using System;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IClientCredentialTokeService
    {
        Task<String> GetToken();
    }
}
