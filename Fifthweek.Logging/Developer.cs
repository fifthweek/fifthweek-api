namespace Fifthweek.Logging
{
    public class Developer
    {
        public Developer(string gitName, string slackName, string fifthweekEmail)
        {
            this.GitName = gitName;
            this.SlackName = slackName;
            this.FifthweekEmail = fifthweekEmail;
        }

        public string GitName { get; private set; }

        public string SlackName { get; private set; }

        public string FifthweekEmail { get; private set; }
    }
}