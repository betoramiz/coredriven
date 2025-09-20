using CoreDriven.Application.Common.Storage;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using NanoidDotNet;

namespace CoreDriven.Infrastructure.Storage;

public class MinioStorage: IStorageService
{
    private readonly IOptions<S3Configuration> _options;
    public IMinioClient minioClient;

    public MinioStorage(IOptions<S3Configuration> options)
    {
        _options = options;
        minioClient = new MinioClient()
            .WithEndpoint(options.Value.Url)
            .WithCredentials(options.Value.AccessKey, options.Value.SecretKey)
            .Build();
    }
    
    public async Task<ErrorOr<(string, string)>> UploadFile(IFormFile file)
    {
        var created = await StorageOperations.CreateBucketIfNotExistAsync(minioClient, _options.Value.BucketName);
        if(created.IsError)
            return created.Errors;
        
        using var fileStream = new MemoryStream();
        await file.CopyToAsync(fileStream);

        var objectName = Nanoid.Generate(Nanoid.Alphabets.LowercaseLettersAndDigits);
        var type = file.FileName.Split(".")[^1];
        var filename = $"{objectName}.{type}";
        var createObject = await StorageOperations.CreateObjectAsync(minioClient, _options.Value.BucketName, filename, fileStream, file.ContentType);
        if(createObject.IsError)
            return createObject.Errors;
        
        return (ObjectName: filename, Url: $"{_options.Value.BucketName}/{filename}");
    }

    public async Task<ErrorOr<Success>> Remove(string fileName)
    {
        var deleteObject = await StorageOperations.DeleteObjectAsync(minioClient, _options.Value.BucketName, fileName);
        if(deleteObject.IsError)
            return deleteObject.Errors;
        
        return Result.Success;
    }
}