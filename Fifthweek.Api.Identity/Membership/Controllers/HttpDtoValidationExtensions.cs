using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    public static class HttpDtoValidationExtensions
    {
        public static Username AsUsername(this string value, string key, ModelStateDictionary modelStateDictionary, bool isRequired = true)
        {
            if (!isRequired && string.IsNullOrWhiteSpace(value)) // Usernames are whitespace insensitive.
            {
                // Optional field.
                return null;
            }

            Username username;
            IReadOnlyCollection<string> errorMessages;
            if (Username.TryParse(value, out username, out errorMessages))
            {
                return username;
            }

            AddErrorsToModelState(key, modelStateDictionary, errorMessages);
            return null;
        }

        public static Email AsEmail(this string value, string key, ModelStateDictionary modelStateDictionary, bool isRequired = true)
        {
            if (!isRequired && string.IsNullOrWhiteSpace(value)) // Emails are whitespace insensitive.
            {
                // Optional field.
                return null;
            }

            Email email;
            IReadOnlyCollection<string> errorMessages;
            if (Email.TryParse(value, out email, out errorMessages))
            {
                return email;
            }

            AddErrorsToModelState(key, modelStateDictionary, errorMessages);
            return null;
        }

        public static Password AsPassword(this string value, string key, ModelStateDictionary modelStateDictionary, bool isRequired = true)
        {
            if (!isRequired && string.IsNullOrEmpty(value))
            {
                // Optional field.
                return null;
            }

            Password password;
            IReadOnlyCollection<string> errorMessages;
            if (Password.TryParse(value, out password, out errorMessages))
            {
                return password;
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