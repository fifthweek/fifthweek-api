namespace Fifthweek.Shared
{
    using System.Collections.Generic;
    using System.Linq;

    public static class FilePurposes
    {
        public const string ProfileImage = "profile-image";
        public const string ProfileHeaderImage = "profile-header-image";

        private static readonly Dictionary<string, FilePurpose> Items = new Dictionary<string, FilePurpose>();

        static FilePurposes()
        {
            Add(ProfileImage, true);
            Add(ProfileHeaderImage, true);
        }

        public static FilePurpose TryGetFilePurpose(string purpose)
        {
            if (string.IsNullOrWhiteSpace(purpose))
            {
                return null;
            }

            FilePurpose result;
            Items.TryGetValue(purpose, out result);
            return result;
        }

        public static IEnumerable<FilePurpose> GetAll()
        {
            return Items.Values.ToList();
        }

        private static void Add(string purpose, bool isPublic)
        {
            Items.Add(purpose, new FilePurpose(purpose, isPublic));
        }
    }
}