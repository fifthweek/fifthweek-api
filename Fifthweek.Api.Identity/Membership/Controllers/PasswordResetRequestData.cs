namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System.ComponentModel.DataAnnotations;

    public class PasswordResetRequestData
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

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