using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Learning_Management_System.Extensions
{
    public class FileUpLoading
    {
        public string UploadImage(IFormFile? imageFile)
        {
            using var stream = imageFile.OpenReadStream();
            var account = new Account("dqnsplymn", "279175116359664", "Oii8kBOmGAaOw_Wadnp0Rwc9oFk");
            var cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageFile.FileName, stream)
            };
            var result = cloudinary.Upload(uploadParams);
            var imageUrl = result.SecureUrl.OriginalString.ToString();
            return imageUrl;
        }
        public string UploadVideo(IFormFile? imageFile)
        {
            using var stream = imageFile.OpenReadStream();
            var account = new Account("dqnsplymn", "279175116359664", "Oii8kBOmGAaOw_Wadnp0Rwc9oFk");
            var cloudinary = new Cloudinary(account);
            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(imageFile.FileName, stream),
                EagerTransforms = new List<Transformation>()
                {
                    new EagerTransformation().Width(300).Height(300).Crop("pad").AudioCodec("none"),
                    new EagerTransformation().Width(160).Height(100).Crop("crop").Gravity("south").AudioCodec("none"),
                },
                //EagerAsync = true,
                //EagerNotificationUrl = "https://mysite.example.com/my_notification_endpoint"
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            var videoUrl = uploadResult.SecureUrl.OriginalString.ToString();
            return videoUrl;
        }
    }
}
