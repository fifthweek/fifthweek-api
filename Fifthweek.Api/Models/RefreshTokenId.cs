namespace Fifthweek.Api.Models
{
    using System;

    public class RefreshTokenId
    {
        public RefreshTokenId(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }

        public static RefreshTokenId Create()
        {
            return new RefreshTokenId(Guid.NewGuid().ToString("n"));
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}