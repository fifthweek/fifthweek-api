namespace Dexter.Api.QueryHandlers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Dexter.Api.Models;
    using Dexter.Api.Queries;

    public class GetVerifiedAccessTokenQueryHandler : IQueryHandler<GetVerifiedAccessTokenQuery, ParsedExternalAccessToken>
    {
        public async Task<ParsedExternalAccessToken> HandleAsync(GetVerifiedAccessTokenQuery query)
        {
            ParsedExternalAccessToken parsedToken = null;

            var provider = query.Provider;
            var accessToken = query.AccessToken;

            string verifyTokenEndPoint;

            if (provider == "Facebook")
            {
                // You can get it from here: https://developers.facebook.com/tools/accesstoken/
                // More about debug_tokn here: http://stackoverflow.com/questions/16641083/how-does-one-get-the-app-access-token-for-debug-token-inspection-on-facebook
                const string AppToken = "561913473930746|O1q3gr_5LfSgVyJy4aR1eWH0nEk";
                verifyTokenEndPoint = string.Format(@"https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, AppToken);
            }
            else
            {
                return null;
            }

            var client = new HttpClient();
            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                parsedToken = new ParsedExternalAccessToken();

                if (provider == "Facebook")
                {
                    parsedToken.UserId = jsonObject["data"]["user_id"];
                    parsedToken.ApplicationId = jsonObject["data"]["app_id"];

                    if (!string.Equals(OAuthConfig.FacebookAuthenticationOptions.AppId, parsedToken.ApplicationId, StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                }
            }

            return parsedToken;
        }
    }
}