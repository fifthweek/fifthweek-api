namespace Fifthweek.Payments.Taxamo
{
    public interface ITaxamoApiKeyRepository
    {
        string GetTestApiKey();

        string GetLiveApiKey();

        string GetApiKey(UserType userType);
    }
}