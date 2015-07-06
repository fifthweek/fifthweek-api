namespace Taxamo.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestSharp;

    using Taxamo.Client;
    using Taxamo.Model;

    public interface IApivverificationApi {
    
    /// <summary>
    /// Create SMS token 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CreateSMSTokenOut</returns>
    CreateSMSTokenOut CreateSMSToken (CreateSMSTokenIn Input);

    /// <summary>
    /// Create SMS token 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CreateSMSTokenOut</returns>
    Task<CreateSMSTokenOut> CreateSMSTokenAsync (CreateSMSTokenIn Input);
    
    /// <summary>
    /// Verify SMS token 
    /// </summary>
    /// <param name="Token">Provided token.</param>
    /// <returns>VerifySMSTokenOut</returns>
    VerifySMSTokenOut VerifySMSToken (string Token);

    /// <summary>
    /// Verify SMS token 
    /// </summary>
    /// <param name="Token">Provided token.</param>
    /// <returns>VerifySMSTokenOut</returns>
    Task<VerifySMSTokenOut> VerifySMSTokenAsync (string Token);
    
  }

  /// <summary>
  /// Represents a collection of functions to interact with the API endpoints
  /// </summary>
  public class ApivverificationApi : IApivverificationApi {

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivverificationApi"/> class.
    /// </summary>
    /// <param name="apiClient"> an instance of ApiClient (optional)
    /// <returns></returns>
    public ApivverificationApi(ApiClient apiClient = null) {
      if (apiClient == null) { // use the default one in Configuration
        this.apiClient = Configuration.apiClient; 
      } else {
        this.apiClient = apiClient;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivverificationApi"/> class.
    /// </summary>
    /// <returns></returns>
    public ApivverificationApi(String basePath)
    {
      this.apiClient = new ApiClient(basePath);
    }

    /// <summary>
    /// Sets the base path of the API client.
    /// </summary>
    /// <value>The base path</value>
    public void SetBasePath(String basePath) {
      this.apiClient.basePath = basePath;
    }

    /// <summary>
    /// Gets the base path of the API client.
    /// </summary>
    /// <value>The base path</value>
    public String GetBasePath(String basePath) {
      return this.apiClient.basePath;
    }

    /// <summary>
    /// Gets or sets the API client.
    /// </summary>
    /// <value>The API client</value>
    public ApiClient apiClient {get; set;}


    
    /// <summary>
    /// Create SMS token 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CreateSMSTokenOut</returns>
    public CreateSMSTokenOut CreateSMSToken (CreateSMSTokenIn Input) {

      
      // verify the required parameter 'Input' is set
      if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling CreateSMSToken");
      

      var path = "/api/v1/verification/sms";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

      
      
      
      postBody = this.apiClient.Serialize(Input); // http body (model) parameter
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling CreateSMSToken: " + response.Content, response.Content);
      }
      return (CreateSMSTokenOut) this.apiClient.Deserialize(response.Content, typeof(CreateSMSTokenOut));
    }
	
	 /// <summary>
    /// Create SMS token 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CreateSMSTokenOut</returns>
    public async Task<CreateSMSTokenOut> CreateSMSTokenAsync (CreateSMSTokenIn Input) {

      
          // verify the required parameter 'Input' is set
          if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling CreateSMSToken");
      

      var path = "/api/v1/verification/sms";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

      
      
      
      postBody = this.apiClient.Serialize(Input); // http body (model) parameter
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling CreateSMSToken: " + response.Content, response.Content);
      }
      return (CreateSMSTokenOut) this.apiClient.Deserialize(response.Content, typeof(CreateSMSTokenOut));
    }
    
    /// <summary>
    /// Verify SMS token 
    /// </summary>
    /// <param name="Token">Provided token.</param>
    /// <returns>VerifySMSTokenOut</returns>
    public VerifySMSTokenOut VerifySMSToken (string Token) {

      
      // verify the required parameter 'Token' is set
      if (Token == null) throw new ApiException(400, "Missing required parameter 'Token' when calling VerifySMSToken");
      

      var path = "/api/v1/verification/sms/{token}";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "token" + "}", this.apiClient.ParameterToString(Token));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling VerifySMSToken: " + response.Content, response.Content);
      }
      return (VerifySMSTokenOut) this.apiClient.Deserialize(response.Content, typeof(VerifySMSTokenOut));
    }
	
	 /// <summary>
    /// Verify SMS token 
    /// </summary>
    /// <param name="Token">Provided token.</param>
    /// <returns>VerifySMSTokenOut</returns>
    public async Task<VerifySMSTokenOut> VerifySMSTokenAsync (string Token) {

      
          // verify the required parameter 'Token' is set
          if (Token == null) throw new ApiException(400, "Missing required parameter 'Token' when calling VerifySMSToken");
      

      var path = "/api/v1/verification/sms/{token}";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "token" + "}", this.apiClient.ParameterToString(Token));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling VerifySMSToken: " + response.Content, response.Content);
      }
      return (VerifySMSTokenOut) this.apiClient.Deserialize(response.Content, typeof(VerifySMSTokenOut));
    }
    
  }  
  
}
