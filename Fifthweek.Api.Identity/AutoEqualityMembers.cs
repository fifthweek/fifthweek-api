
namespace Fifthweek.Api.Identity.Membership.Commands
{
	public partial class ConfirmPasswordResetCommand
	{
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

            return this.Equals((ConfirmPasswordResetCommand)obj);
        }

		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
				hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.Token != null ? this.Token.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.NewPassword != null ? this.NewPassword.GetHashCode() : 0);
                return hashCode;
            }
        }

		protected bool Equals(ConfirmPasswordResetCommand other)
        {
			if (!object.Equals(this.UserId, other.UserId))
			{
				return false;
			}
			if (!object.Equals(this.Token, other.Token))
			{
				return false;
			}
			if (!object.Equals(this.NewPassword, other.NewPassword))
			{
				return false;
			}
			return true;
        }
	}
}
