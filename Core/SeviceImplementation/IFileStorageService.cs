using Microsoft.AspNetCore.Http;
using Shared;
using Shared.Enums;

namespace SeviceAbstraction
{
    public interface IFileStorageService
    {
        Task<FileUploadResult> UploadAsync(IFormFile file, UploadFolder folder);

        Task DeleteAsync(string relativePath);
    }
}
