

using Microsoft.AspNetCore.Http;

namespace Forum.API.BL.Services
{
    public static class ImageConverter
    {
        public static string ConvertImage(this IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
