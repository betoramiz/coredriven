using ErrorOr;
using Minio;
using Minio.DataModel.Args;

namespace CoreDriven.Infrastructure.Storage;

public class StorageOperations
{
    public static async Task<ErrorOr<Success>> CreateBucketIfNotExistAsync(IMinioClient client, string bucketName)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(bucketName);
            var found = await client.BucketExistsAsync(bucketExistArgs);
            if (!found)
            {
                var mbArgs = new MakeBucketArgs().WithBucket(bucketName);
                await client.MakeBucketAsync(mbArgs);
            }
            return Result.Success;
        }
        catch (Exception e)
        {
            return Errors.CreateBucket;
        }
        
    }

    public static async Task<ErrorOr<Success>> CreateObjectAsync(IMinioClient client, string bucketName, string fileName, MemoryStream fileStream, string contentType)
    {
        try
        {
            var fileBytes = fileStream.ToArray();

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithStreamData(new MemoryStream(fileBytes))
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType);
        
            var response = await client.PutObjectAsync(putObjectArgs);
            
            return Result.Success;
        }
        catch (Exception e)
        {
            return Errors.UploadError;
        }
        
    }

    public static async Task<ErrorOr<Success>> DeleteObjectAsync(IMinioClient client, string bucketName,
        string fileName)
    {
        try
        {
            var deleteObjectArgs = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName);
        
            await client.RemoveObjectAsync(deleteObjectArgs);
            return Result.Success;
        }
        catch (Exception e)
        {
            return Errors.RemoveError;
        }
    }
}