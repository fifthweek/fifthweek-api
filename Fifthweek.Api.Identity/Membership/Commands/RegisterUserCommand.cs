using System;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoEqualityMembers]
    public partial class RegisterUserCommand
    {
        public RegisterUserCommand(UserId userId, string exampleWork, NormalizedEmail email, NormalizedUsername username, Password password)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }
            
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            this.UserId = userId;
            this.ExampleWork = exampleWork;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }

        public UserId UserId { get; private set; }

        public string ExampleWork { get; private set; }

        public NormalizedEmail Email { get; private set; }

        public NormalizedUsername Username { get; private set; }

        public Password Password { get; private set; }
    }
}