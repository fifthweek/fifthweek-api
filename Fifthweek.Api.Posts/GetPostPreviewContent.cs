namespace Fifthweek.Api.Posts
{
    using System.Text.RegularExpressions;

    using Fifthweek.Api.Posts.Shared;

    using Newtonsoft.Json;

    public class GetPostPreviewContent : IGetPostPreviewContent
    {
        // https://en.wikipedia.org/wiki/Asterism_(typography)
        private const string ReplacementCharacter = "⁂";

        public string Execute(string postContent, PreviewText previewText)
        {
            if (string.IsNullOrWhiteSpace(postContent))
            {
                return string.Empty;
            }

            var regex = new Regex(@"\S");
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
                                data.text = previewText.Value + regex.Replace(text, ReplacementCharacter);
                            }
                            else
                            {
                                data.text = regex.Replace(text, ReplacementCharacter);
                            }

                            isFirstText = false;
                        }
                    }
                }
            }

            postContent = JsonConvert.SerializeObject(result);
            return postContent;
        }
    }
}