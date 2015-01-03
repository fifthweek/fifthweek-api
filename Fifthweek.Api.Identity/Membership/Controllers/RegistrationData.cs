using System.Web.Http.ModelBinding;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System.ComponentModel.DataAnnotations;

    public class RegistrationData
    {
        [Display(Name = "ExampleWork")]
        public string ExampleWork { get; set; }

        public string Email { get; set; }

        public Email EmailObj { get; private set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(20, MinimumLength = 6)]
        [RegularExpression(@"[a-zA-Z0-9-_]+", ErrorMessage = "Only alphanumeric characters, underscores and hyphens are allowed in the username.")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public void Parse()
        {
            var modelState = new ModelStateDictionary();
            Email email;
            if (Membership.Email.TryParse(this.Email, out email))
            {
                this.EmailObj = email;
            }
            else
            {
                var error = new ModelState();
                error.Errors.Add("Valid email required");
                modelState.Add("Email", error);
            }

            if (!modelState.IsValid)
            {
                throw new ModelValidationException(modelState);
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