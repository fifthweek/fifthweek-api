namespace Fifthweek.Api.Posts
{
    using System.Text.RegularExpressions;

    using Fifthweek.Api.Posts.Shared;

    using Newtonsoft.Json;

    public class GetPostPreviewContent : IGetPostPreviewContent
    {
        // https://en.wikipedia.org/wiki/Asterism_(typography)
        private const string ReplacementCharacter = "⁂";

        private static readonly Regex CharacterReplaceRegex = new Regex(@"\S");
        private static readonly Regex LinkReplaceRegex = new Regex(@"\[([^\]]+)\]\([^)]+\)");

        public string Execute(string postContent, PreviewText previewText)
        {
            if (string.IsNullOrWhiteSpace(postContent))
            {
                return string.Empty;
            }

            dynamic result = JsonConvert.DeserializeObject(postContent);
            bool isFirstText = true;
            foreach (var item in result)
            {
                if (item.type == "text")
                {
                    var data = item.data;
                    if (data != null)
                    {
                        string text = data.text;

                        if (text != null)
                        {
                            if (isFirstText && previewText != null)
                            {
                                text = text.Substring(previewText.Value.Length);
                                data.text = previewText.Value + this.Obfuscate(text);
                            }
                            else
                            {
                                data.text = this.Obfuscate(text);
                            }

                            isFirstText = false;
                        }
                    }
                }
            }

            postContent = JsonConvert.SerializeObject(result);
            return postContent;
        }

        private string Obfuscate(string text)
        {
            var linksRemoved = LinkReplaceRegex.Replace(text, v => v.Groups[1].Value);
            var result = CharacterReplaceRegex.Replace(linksRemoved, ReplacementCharacter);
            return result;
        }
    }
}