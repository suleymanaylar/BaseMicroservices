using FreeCourse.Web.Models.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();

        Task<List<CategoryViewModel>> GetAllCategoryAsync();

        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);



        Task<CourseViewModel> GetByCourseIdAsync(string courseId);
        Task<bool> DeleteCourseAsync(string courseId);
        Task<bool> CreateCourseAsyn(CreateCourseInput courseCreateInput);

        Task<bool> UpdateCourseAsyn(CourseUpdateInput courseUpdateInput);
    }
}
