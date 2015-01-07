using System;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoEqualityMembers]
    public partial class ConfirmPasswordResetCommand
    {
        public ConfirmPasswordResetCommand(UserId userId, string token, Password newPassword)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            if (newPassword == null)
            {
                throw new ArgumentNullException("newPassword");
            }

            this.UserId = userId;
            this.Token = token;
            this.NewPassword = newPassword;
        }

        public UserId UserId { get; private set; }

        public string Token { get; private set; }

        public Password NewPassword { get; private set; }
    }
}