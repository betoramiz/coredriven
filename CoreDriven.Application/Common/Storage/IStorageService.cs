using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace CoreDriven.Application.Common.Storage;

public interface IStorageService
{
    Task<ErrorOr<(string, string)>> UploadFile(IFormFile file);
    Task<ErrorOr<Success>> Remove(string fileName);
}