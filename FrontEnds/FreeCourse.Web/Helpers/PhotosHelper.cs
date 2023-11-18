using FreeCourse.Web.Models;
using Microsoft.Extensions.Options;

namespace FreeCourse.Web.Helpers
{
    public class PhotosHelper
    {
        private readonly ServicesApiSettings _servicesApiSettings;

        public PhotosHelper(IOptions<ServicesApiSettings> servicesApiSettings)
        {
            _servicesApiSettings = servicesApiSettings.Value;
        }
        public string GetPhotoStockUrl(string photoUrl)
        {
            return $"{_servicesApiSettings.PhotoStockUrl}/photos/{photoUrl}"; ;
        }
    }
}
