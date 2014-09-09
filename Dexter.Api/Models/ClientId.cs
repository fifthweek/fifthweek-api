namespace Dexter.Api.Models
{
    public class ClientId
    {
        public ClientId(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }
    }
}