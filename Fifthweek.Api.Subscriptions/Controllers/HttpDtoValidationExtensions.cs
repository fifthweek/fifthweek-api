using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace Fifthweek.Api.Subscriptions.Controllers
{
    public static class HttpDtoValidationExtensions
    {
        public static SubscriptionName AsSubscriptionName(this string value, string key, ModelStateDictionary modelStateDictionary, bool isRequired = true)
        {
            if (!isRequired && string.IsNullOrWhiteSpace(value))
            {
                // Optional field.
                return null;
            }

            SubscriptionName obj;
            IReadOnlyCollection<string> errorMessages;
            if (SubscriptionName.TryParse(value, out obj, out errorMessages))
            {
                return obj;
            }

            AddErrorsToModelState(key, modelStateDictionary, errorMessages);
            return null;
        }

        public static Tagline AsTagline(this string value, string key, ModelStateDictionary modelStateDictionary, bool isRequired = true)
        {
            if (!isRequired && string.IsNullOrWhiteSpace(value))
            {
                // Optional field.
                return null;
            }

            Tagline obj;
            IReadOnlyCollection<string> errorMessages;
            if (Tagline.TryParse(value, out obj, out errorMessages))
            {
                return obj;
            }

            AddErrorsToModelState(key, modelStateDictionary, errorMessages);
            return null;
        }

        public static ChannelPriceInUsCentsPerWeek AsChannelPriceInUsCentsPerWeek(this int value, string key, ModelStateDictionary modelStateDictionary, bool isRequired = true)
        {
            ChannelPriceInUsCentsPerWeek obj;
            IReadOnlyCollection<string> errorMessages;
            if (ChannelPriceInUsCentsPerWeek.TryParse(value, out obj, out errorMessages))
            {
                return obj;
            }

            AddErrorsToModelState(key, modelStateDictionary, errorMessages);
            return null;
        }

        private static void AddErrorsToModelState(string key, ModelStateDictionary modelStateDictionary, IEnumerable<string> errorMessages)
        {
            var modelState = new ModelState();
            foreach (var errorMessage in errorMessages)
            {
                modelState.Errors.Add(errorMessage);
            }

            modelStateDictionary.Add(key, modelState);
        }
    }
}