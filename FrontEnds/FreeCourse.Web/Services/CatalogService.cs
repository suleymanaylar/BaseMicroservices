using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateCourseAsyn(CreateCourseInput courseCreateInput)
        {
            var response = await _httpClient.PostAsJsonAsync<CreateCourseInput>("courses",courseCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {

            var response = await _httpClient.DeleteAsync($"courses/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
                return null;

            var responseSucces = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSucces.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
                return null;

            var responseSucces =await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return responseSucces.Data;
        }
        //"/api/[controller]/GetAllByUserId/{userId}"
        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var responseSucces = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            return responseSucces.Data;
        }

        public async Task<CourseViewModel> GetByCourseIdAsync(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var responseSucces = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            return responseSucces.Data;
        }

        public async Task<bool> UpdateCourseAsyn(CourseUpdateInput courseUpdateInput)
        {
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
