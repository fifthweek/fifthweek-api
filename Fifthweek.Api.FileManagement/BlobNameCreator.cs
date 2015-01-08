namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Text;

    public class BlobNameCreator : IBlobNameCreator
    {
        private static readonly Random Random = new Random();

        public string CreateFileName()
        {
            var sb = new StringBuilder();
            sb.Append(DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss-fff"));
            sb.Append('-');

            this.AppendRandomString(sb, 6);
         
            return sb.ToString();
        }

        internal void AppendRandomString(StringBuilder sb, int length)
        {
            for (int i = 0; i < length; i++)
            {
                sb.Append(this.GenerateRandomCharacter(Random.Next(32)));
            }
        }

        internal char GenerateRandomCharacter(int value)
        {
            if (value < 0 || value > 31)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            char result = (char)(value + 'a');

            if (result > 'z')
            {
                result = (char)(result - 'z' + '1');
            }

            return result;
        }
    }
}