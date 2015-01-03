using System.Web.Http.ModelBinding;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    public class PasswordResetRequestData
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public Email EmailObj { get; private set; }

        public Username UsernameObj { get; private set; }

        public void Parse()
        {
            var modelState = new ModelStateDictionary();

            this.UsernameObj = this.Username.AsUsername("Username", modelState, isRequired: false);
            this.EmailObj = this.Email.AsEmail("Email", modelState, isRequired: false);

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

            return this.Equals((PasswordResetRequestData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Email != null ? this.Email.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(PasswordResetRequestData other)
        {
            return string.Equals(this.Email, other.Email) &&
                   string.Equals(this.Username, other.Username);
        }
    }
}