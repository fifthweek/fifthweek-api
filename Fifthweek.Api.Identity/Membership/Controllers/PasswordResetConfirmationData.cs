using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.ModelBinding;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    public class PasswordResetConfirmationData
    {
        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "Token")]
        public string Token { get; set; }

        public string NewPassword { get; set; }

        public UserId UserIdObj { get; private set; }

        public Password NewPasswordObj { get; private set; }

        public void Parse()
        {
            var modelState = new ModelStateDictionary();

            this.UserIdObj = Membership.UserId.Parse(this.UserId);
            this.NewPasswordObj = this.NewPassword.AsPassword("NewPassword", modelState);

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

            return this.Equals((PasswordResetConfirmationData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Token != null ? this.Token.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPassword != null ? this.NewPassword.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(PasswordResetConfirmationData other)
        {
            return string.Equals(this.UserId, other.UserId) &&
                   string.Equals(this.Token, other.Token) &&
                   string.Equals(this.NewPassword, other.NewPassword);
        }
    }
}