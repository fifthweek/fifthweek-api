namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This is a first attempt at URL validation. When we come to write the embed code generator, we will have a better idea of what valid URLs are. At this point,
    /// we will be able to update the following rules to meet the generator's requirements.
    /// </summary>
    public partial class ValidExternalVideoUrl : ExternalVideoUrl
    {
        public static readonly int MinLength = 1;
        public static readonly int MaxLength = 100; // Set quite high to allow Vimeo's vanity URLs. Right now it seems unlikely someone will pick a long / hard to remember vanity URL.

        public static readonly HashSet<string> AllowedDomains = new HashSet<string>(new[]
        {
            "www.youtube.com",
            "youtube.com",
            "youtu.be",
            "www.vimeo.com",
            "vimeo.com",
        });

        private const string AllowedDomainsMessage = "Vimeo or YouTube";

        private ValidExternalVideoUrl(string value)
            : base(value)
        {
        }

        public static bool IsEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static ValidExternalVideoUrl Parse(string value)
        {
            ValidExternalVideoUrl retval;
            IReadOnlyCollection<string> errorMessages;
            if (!TryParse(value, out retval, out errorMessages))
            {
                throw new ArgumentException("Invalid external video URL", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ValidExternalVideoUrl externalVideoUrl)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out externalVideoUrl, out errorMessages);
        }

        public static bool TryParse(string value, out ValidExternalVideoUrl externalVideoUrl, out IReadOnlyCollection<string> errorMessages)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (IsEmpty(value))
            {
                // TryParse should never fail, so report null as an error instead of ArgumentNullException.
                errorMessageList.Add("Value required");
            }
            else
            {
                Uri uri;
                if (!Uri.TryCreate(value, UriKind.Absolute, out uri))
                {
                    errorMessageList.Add("Must be valid URL");
                }
                else
                {
                    value = uri.ToString();

                    if (value.Length < MinLength || value.Length > MaxLength)
                    {
                        errorMessageList.Add(string.Format("Length must be from {0} to {1} characters", MinLength, MaxLength));
                    }

                    if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
                    {
                        errorMessageList.Add("Must be HTTP(S)");
                    }

                    if (!AllowedDomains.Contains(uri.Host))
                    {
                        errorMessageList.Add("Must be from " + AllowedDomainsMessage);
                    }
                }
            }

            if (errorMessageList.Count > 0)
            {
                externalVideoUrl = null;
                return false;
            }

            externalVideoUrl = new ValidExternalVideoUrl(value);

            return true;
        }
    }
}