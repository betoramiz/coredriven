namespace CoreDriven.Infrastructure.Storage;

public class S3Configuration
{
    public const string Option = "S3Configuration";
    
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty;
}