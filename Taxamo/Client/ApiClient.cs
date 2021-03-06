namespace Taxamo.Client {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using RestSharp;

    /// <summary>
  /// API client is mainly responible for making the HTTP call to the API backend
  /// </summary>
  public class ApiClient {

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiClient"/> class.
    /// </summary>
    /// <param name="basePath">The base path.</param>
    public ApiClient(string apiKey, String basePath="https://api.taxamo.com/") {
        this.ApiKey = apiKey;
      this.basePath = basePath;
      this.restClient = new RestClient(this.basePath);
    }

    /// <summary>
    /// Gets or sets the base path.
    /// </summary>
    /// <value>The base path.</value> 
    public string basePath { get; set; }

        public string ApiKey { get; private set; }

        /// <summary>
    /// Gets or sets the RestClient
    /// </summary>
    /// <value>The RestClient.</value> 
    public RestClient restClient { get; set; }

    private Dictionary<String, String> defaultHeaderMap = new Dictionary<String, String>();

    public Object CallApi(String Path, RestSharp.Method Method, Dictionary<String, String> QueryParams, String PostBody,
      Dictionary<String, String> HeaderParams, Dictionary<String, String> FormParams, Dictionary<String, String> FileParams, String[] AuthSettings) {
      var response = Task.Run(async () => {
                            var resp = await this.CallApiAsync(Path, Method, QueryParams, PostBody, HeaderParams, FormParams, FileParams, AuthSettings);
                            return resp;
                            });
        return response.Result;
    }

    public async Task<Object> CallApiAsync(String Path, RestSharp.Method Method, Dictionary<String, String> QueryParams, String PostBody,
      Dictionary<String, String> HeaderParams, Dictionary<String, String> FormParams, Dictionary<String, String> FileParams, String[] AuthSettings) {

      var request = new RestRequest(Path, Method);

        HeaderParams["Token"] = this.ApiKey;

      // add default header, if any
      foreach(KeyValuePair<string, string> defaultHeader in this.defaultHeaderMap)
        request.AddHeader(defaultHeader.Key, defaultHeader.Value);

      // add header parameter, if any
      foreach(KeyValuePair<string, string> param in HeaderParams)
        request.AddHeader(param.Key, param.Value);
     
      // add query parameter, if any
      foreach(KeyValuePair<string, string> param in QueryParams)
        request.AddQueryParameter(param.Key, param.Value);

      // add form parameter, if any
      foreach(KeyValuePair<string, string> param in FormParams)
        request.AddParameter(param.Key, param.Value);

      // add file parameter, if any
      foreach(KeyValuePair<string, string> param in FileParams)
        request.AddFile(param.Key, param.Value);

      if (PostBody != null) {
        request.AddParameter("application/json", PostBody, ParameterType.RequestBody); // http body (model) parameter
      }

      return (Object) await this.restClient.ExecuteTaskAsync(request);

    }

    /// <summary>
    /// Add default header
    /// </summary>
    /// <param name="key">  Header field name
    /// <param name="value"> Header field value
    /// <returns></returns>
    public void AddDefaultHeader(string key, string value) {
       this.defaultHeaderMap.Add(key, value);
    }

    /// <summary>
    /// Get default header
    /// </summary>
    /// <returns>Dictionary of default header</returns>
    public Dictionary<String, String> GetDefaultHeader() {
       return this.defaultHeaderMap;
    }

    /// <summary>
    /// escape string (url-encoded)
    /// </summary>
    /// <param name="str"> String to be escaped
    /// <returns>Escaped string</returns>
    public string EscapeString(string str) {
      return str;
    }

    /// <summary>
    /// if parameter is DateTime, output in ISO8601 format
    /// if parameter is a list of string, join the list with ","
    /// otherwise just return the string
    /// </summary>
    /// <param name="obj"> The parameter (header, path, query, form)
    /// <returns>Formatted string</returns>
    public string ParameterToString(object obj)
    {
      if (obj is DateTime) {
        return ((DateTime)obj).ToString ("u");
      } else if (obj is List<string>) {
        return String.Join(",", obj as List<string>);
      } else {
	return Convert.ToString (obj);
      }
    }

    /// <summary>
    /// Deserialize the JSON string into a proper object
    /// </summary>
    /// <param name="json"> JSON string
    /// <param name="type"> Object type
    /// <returns>Object representation of the JSON string</returns>
    public object Deserialize(string content, Type type) {
      if (type.GetType() == typeof(Object))
        return (Object)content;

      try
      {
          return JsonConvert.DeserializeObject(content, type);
      }
      catch (IOException e) {
        throw new ApiException(500, e.Message);
      }
    }

    /// <summary>
    /// Serialize an object into JSON string
    /// </summary>
    /// <param name="obj"> Object 
    /// <returns>JSON string</returns>
    public string Serialize(object obj) {
      try
      {
          return obj != null ? JsonConvert.SerializeObject(obj) : null;
      }
      catch (Exception e) {
        throw new ApiException(500, e.Message);
      }
    }


    /// <summary>
    /// Encode string in base64 format 
    /// </summary>
    /// <param name="text">String to be encoded</param>
    public static string Base64Encode(string text) {
      var textByte = System.Text.Encoding.UTF8.GetBytes(text);
      return System.Convert.ToBase64String(textByte);
    }

  }
}
