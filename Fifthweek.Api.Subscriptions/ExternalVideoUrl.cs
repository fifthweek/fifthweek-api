namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This is a first attempt at URL validation. When we come to write the embed code generator, we will have a better idea of what valid URLs are. At this point,
    /// we will be able to update the following rules to meet the generator's requirements.
    /// </summary>
    public class ExternalVideoUrl
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

        private ExternalVideoUrl()
        {
        }

        public string Value { get; protected set; }

        public static bool IsEmpty(string value, bool exact = false)
        {
            return exact ? string.IsNullOrEmpty(value) : string.IsNullOrWhiteSpace(value);
        }

        public static ExternalVideoUrl Parse(string value, bool exact = false)
        {
            ExternalVideoUrl retval;
            IReadOnlyCollection<string> errorMessages;
            if (!TryParse(value, out retval, out errorMessages, exact))
            {
                throw new ArgumentException("Invalid external video URL", "value");
            }

            return retval;
        }

        public static bool TryParse(string value, out ExternalVideoUrl externalVideoUrl, bool exact = false)
        {
            IReadOnlyCollection<string> errorMessages;
            return TryParse(value, out externalVideoUrl, out errorMessages, exact);
        }

        public static bool TryParse(string value, out ExternalVideoUrl externalVideoUrl, out IReadOnlyCollection<string> errorMessages, bool exact = false)
        {
            var errorMessageList = new List<string>();
            errorMessages = errorMessageList;

            if (IsEmpty(value, exact))
            {
                // Method should never fail, so report null as an error instead of ArgumentNullException.
                errorMessageList.Add("Value required");
            }
            else
            {
                if (!exact)
                {
                    value = value.Trim();
                }

                if (value.Length < MinLength || value.Length > MaxLength)
                {
                    errorMessageList.Add(string.Format("Length must be from {0} to {1} characters", MinLength, MaxLength));
                }

                Uri uri;
                if (!Uri.TryCreate(value, UriKind.Absolute, out uri))
                {
                    errorMessageList.Add("Must be valid URL");
                }
                else
                {
                    if (!exact)
                    {
                        // Gets the 'canonical string representation' - which means to normalize - so it
                        // sets the scheme and authority to lowercase for us :)
                        value = uri.ToString(); 
                    }

                    if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
                    {
                        errorMessageList.Add("Must be HTTP(S)");
                    }

                    if (!AllowedDomains.Contains(uri.Host))
                    {
                        errorMessageList.Add("Must be from " + AllowedDomainsMessage);
                    }

                    if (value.Any(char.IsWhiteSpace))
                    {
                        errorMessageList.Add("No whitespace is allowed");
                    }

                    var leftPart = uri.GetLeftPart(UriPartial.Authority);
                    if (leftPart != value.Substring(0, leftPart.Length))
                    {
                        // Only reason these would differ at these point is case mismatch. Since the value from the
                        // URI type is guaranteed to be lowercase, this means there must be uppercase in the originally
                        // provided value.
                        errorMessageList.Add("No uppercase characters allowed in left part of URL");
                    }
                }
            }

            if (errorMessageList.Count > 0)
            {
                externalVideoUrl = null;
                return false;
            }

            externalVideoUrl = new ExternalVideoUrl
            {
                Value = value
            };

            return true;
        }
    }
}