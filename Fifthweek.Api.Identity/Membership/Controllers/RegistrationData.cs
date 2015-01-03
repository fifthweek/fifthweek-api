using System.Collections.Generic;
using System.Web.Http.ModelBinding;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System.ComponentModel.DataAnnotations;

    public class RegistrationData
    {
        public string ExampleWork { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public Email EmailObj { get; private set; }

        public Username UsernameObj { get; private set; }

        public void Parse()
        {
            var errorDictionary = new Dictionary<string, IReadOnlyCollection<string>>();

            this.ParseEmail(errorDictionary);
            this.ParseUsername(errorDictionary);

            if (errorDictionary.Count <= 0)
            {
                return;
            }

            var modelStateDictionary = new ModelStateDictionary();
            foreach (var kvp in errorDictionary)
            {
                var modelState = new ModelState();
                foreach (var error in kvp.Value)
                {
                    modelState.Errors.Add(error);
                }

                modelStateDictionary.Add(kvp.Key, modelState);
            }
            throw new ModelValidationException(modelStateDictionary);
        }

        private void ParseEmail(IDictionary<string, IReadOnlyCollection<string>> errorDictionary)
        {
            Email email;
            IReadOnlyCollection<string> errorMessages;
            if (Membership.Email.TryParse(this.Email, out email, out errorMessages))
            {
                this.EmailObj = email;
            }
            else
            {
                errorDictionary.Add("Email", errorMessages);
            }
        }

        private void ParseUsername(IDictionary<string, IReadOnlyCollection<string>> errorDictionary)
        {
            Username username;
            IReadOnlyCollection<string> errorMessages;
            if (Membership.Username.TryParse(this.Username, out username, out errorMessages))
            {
                this.UsernameObj = username;
            }
            else
            {
                errorDictionary.Add("Username", errorMessages);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((RegistrationData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.ExampleWork != null ? this.ExampleWork.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Password != null ? this.Password.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(RegistrationData other)
        {
            return string.Equals(this.ExampleWork, other.ExampleWork) &&
                string.Equals(this.Email, other.Email) &&
                string.Equals(this.Username, other.Username) &&
                string.Equals(this.Password, other.Password);
        }
    }
}