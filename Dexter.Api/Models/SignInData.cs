namespace Dexter.Api.Models
{
    using Microsoft.AspNet.Identity;

    public class SignInData
    {
        public SignInData(string provider, string providerKey)
        {
            this.Provider = provider;
            this.ProviderKey = providerKey;
        }

        public string Provider { get; private set; }

        public string ProviderKey { get; private set; }

        public UserLoginInfo ToUserLoginInfo()
        {
            return new UserLoginInfo(this.Provider, this.ProviderKey);
        }
    }
}