namespace Fifthweek.Api.Azure
{
    public interface IBlobProperties
    {
        long Length { get; }

        string ContentType { get; set; }
    }
}