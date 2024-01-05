using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Learning_Management_System.Extensions
{
    public class UpLoadFileImage
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
    }
}
