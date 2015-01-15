namespace Fifthweek.Shared
{
    public class FilePurpose
    {
        public FilePurpose(string name, bool isPublic)
        {
            this.Name = name;
            this.IsPublic = isPublic;
        }

        public string Name { get; private set; }

        public bool IsPublic { get; private set; }
    }
}