using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Helpers;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalogs;
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
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotosHelper _photosHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotosHelper photosHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photosHelper = photosHelper;
        }

        public async Task<bool> CreateCourseAsyn(CreateCourseInput courseCreateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);
            if (resultPhotoService != null)
            {
                courseCreateInput.Picture = resultPhotoService.Url;
            }

            var response = await _httpClient.PostAsJsonAsync<CreateCourseInput>("courses", courseCreateInput);

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
            //http:localhost:5000/services/catalog/courses
            var response = await _httpClient.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
                return null;

            var responseSucces = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            responseSucces.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photosHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseSucces.Data;
        }
        //"/api/[controller]/GetAllByUserId/{userId}"
        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var responseSucces = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSucces.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photosHelper.GetPhotoStockUrl(x.Picture);
            });

            return responseSucces.Data;
        }

        public async Task<CourseViewModel> GetByCourseIdAsync(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var responseSucces = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            responseSucces.Data.StockPictureUrl=_photosHelper.GetPhotoStockUrl(responseSucces.Data.Picture);

            return responseSucces.Data;
        }

        public async Task<bool> UpdateCourseAsyn(CourseUpdateInput courseUpdateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseUpdateInput.PhotoFormFile);
            if (resultPhotoService != null)
            {
                await _photoStockService.DeletePhoto(courseUpdateInput.Picture);
                courseUpdateInput.Picture = resultPhotoService.Url;
            }
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
