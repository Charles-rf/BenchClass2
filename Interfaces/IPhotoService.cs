using CloudinaryDotNet.Actions;

namespace BenchClass.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file, int height, int width);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
