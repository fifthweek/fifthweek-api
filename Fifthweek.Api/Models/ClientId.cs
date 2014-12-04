namespace Fifthweek.Api.Models
{
    public class ClientId
    {
        public ClientId(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }

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

            return this.Equals((ClientId)obj);
        }

        public override int GetHashCode()
        {
            return this.Value != null ? this.Value.GetHashCode() : 0;
        }

        protected bool Equals(ClientId other)
        {
            return string.Equals(this.Value, other.Value);
        }
    }
}