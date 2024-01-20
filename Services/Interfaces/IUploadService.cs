namespace MyWatchShop.Services.Interfaces
{
    public interface IUploadService
    {
        Task<Dictionary<string, string>> ImageUpload(IFormFile photo, string folderName);
    }
}
