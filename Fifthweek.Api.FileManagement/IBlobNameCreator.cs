namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Identity.Membership;

    public interface IBlobNameCreator
    {
        string CreateFileName(FileId fileId);
    }
}