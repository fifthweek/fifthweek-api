namespace Fifthweek.Logging
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly Dictionary<string, Developer> developersByGitName;
        
        public DeveloperRepository()
        {
            var developers = new List<Developer> 
            {
                new Developer("James Thurley", "james", "james+devapierror@fifthweek.com"),
                new Developer("Lawrence Wagerfield", "lawrence", "lawrence+devapierror@fifthweek.com"),
                new Developer("ttbarnes", null, "tony@tonybarnes.me"), // Tony doesn't want slack notifications.
            };

            this.developersByGitName = developers.ToDictionary(v => v.GitName);
        }

        public Task<Developer> TryGetByGitNameAsync(string gitName)
        {
            if (gitName == null)
            {
                return Task.FromResult<Developer>(null);
            }

            Developer result;
            this.developersByGitName.TryGetValue(gitName, out result);
            return Task.FromResult(result);
        }
    }
}