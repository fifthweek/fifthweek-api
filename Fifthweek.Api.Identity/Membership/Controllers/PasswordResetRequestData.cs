using System;
using System.Web.Http.ModelBinding;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System.ComponentModel.DataAnnotations;

    public class PasswordResetRequestData
    {
        public string Email { get; set; }

        public Email EmailObj { get; private set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        public void Parse()
        {
            var modelState = new ModelStateDictionary();
            
            // Email address is optional.
            if (!String.IsNullOrWhiteSpace(this.Email))
            {
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