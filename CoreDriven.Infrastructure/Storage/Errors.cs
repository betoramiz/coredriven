using ErrorOr;

namespace CoreDriven.Infrastructure.Storage;

public static class Errors
{
    public static Error UploadError => Error.Failure("S3.Storage.Photo.Upload", "Cannot save photo to storage");
    public static Error DownloadError => Error.Failure("Photo.Azure.Storage.Download", "Cannot Download from storage");
    public static Error RemoveError => Error.Failure("Photo.Azure.Storage.Remove", "Cannot remove from storage");
    public static Error CreateBucket => Error.Failure("S3.Storage.Bucket.Create", "Cannot create Bucket");
}