namespace Fifthweek.Azure
{
    public interface IBlobProperties
    {
        long Length { get; }

        string ContentType { get; set; }

        string CacheControl { get; set; }
    }
}