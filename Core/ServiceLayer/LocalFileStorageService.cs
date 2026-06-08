using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SeviceAbstraction;
using Shared;
using Shared.Enums;

namespace ServiceImplementation
{
    public class LocalFileStorageService(
        IWebHostEnvironment _environment) : IFileStorageService
    {

        public async Task<FileUploadResult> UploadAsync(IFormFile file, UploadFolder folder)
        {
            if (file is null || file.Length == 0)
                throw new Exception("Invalid file");

            var extension = Path.GetExtension(file.FileName);

            var allowedExtensions = new[]
            {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
             };

            if (!allowedExtensions.Contains(extension.ToLower()))
                throw new BadHttpRequestException("Invalid image format");


            var fileName = $"{Guid.NewGuid()}{extension}";

            var folderName = folder.ToString().ToLower();

            var uploadDirectory = Path.Combine(_environment.WebRootPath, "uploads", folderName);

            Directory.CreateDirectory(uploadDirectory);

            var fullPath =
                Path.Combine(
                    uploadDirectory,
                    fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);

            await file.CopyToAsync(stream);

            return new FileUploadResult
            {
                FileName = fileName,
                Url = $"/uploads/{folderName}/{fileName}",
                Size = file.Length
            };
        }

        public Task DeleteAsync(string relativePath)
        {
            var filePath = Path.Combine(_environment.WebRootPath, relativePath.TrimStart('/'));

            if (File.Exists(filePath))
                File.Delete(filePath);

            return Task.CompletedTask;
        }
    }
}
