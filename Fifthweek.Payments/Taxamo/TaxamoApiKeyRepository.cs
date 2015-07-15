namespace Fifthweek.Payments.Taxamo
{
    public class TaxamoApiKeyRepository : ITaxamoApiKeyRepository
    {
        public string GetTestApiKey()
        {
            return TaxamoConfiguration.GetTestApiKey();
        }

        public string GetLiveApiKey()
        {
            return TaxamoConfiguration.GetLiveApiKey();
        }

        public string GetApiKey(UserType userType)
        {
            return userType == UserType.StandardUser ? this.GetLiveApiKey() : this.GetTestApiKey();
        }
    }
}