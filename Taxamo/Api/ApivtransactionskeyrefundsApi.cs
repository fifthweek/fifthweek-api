namespace Taxamo.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestSharp;

    using Taxamo.Client;
    using Taxamo.Model;

    public interface IApivtransactionskeyrefundsApi {
    
    /// <summary>
    /// Create a refund 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>CreateRefundOut</returns>
    CreateRefundOut CreateRefund (string Key, CreateRefundIn Input);

    /// <summary>
    /// Create a refund 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>CreateRefundOut</returns>
    Task<CreateRefundOut> CreateRefundAsync (string Key, CreateRefundIn Input);
    
  }

  /// <summary>
  /// Represents a collection of functions to interact with the API endpoints
  /// </summary>
  public class ApivtransactionskeyrefundsApi : IApivtransactionskeyrefundsApi {

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivtransactionskeyrefundsApi"/> class.
    /// </summary>
    /// <param name="apiClient"> an instance of ApiClient (optional)
    /// <returns></returns>
    public ApivtransactionskeyrefundsApi(ApiClient apiClient = null) {
      if (apiClient == null) { // use the default one in Configuration
        this.apiClient = Configuration.apiClient; 
      } else {
        this.apiClient = apiClient;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivtransactionskeyrefundsApi"/> class.
    /// </summary>
    /// <returns></returns>
    public ApivtransactionskeyrefundsApi(String basePath)
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
    /// Create a refund 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>CreateRefundOut</returns>
    public CreateRefundOut CreateRefund (string Key, CreateRefundIn Input) {

      
      // verify the required parameter 'Key' is set
      if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling CreateRefund");
      
      // verify the required parameter 'Input' is set
      if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling CreateRefund");
      

      var path = "/api/v1/transactions/{key}/refunds";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "key" + "}", this.apiClient.ParameterToString(Key));
      

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
        throw new ApiException ((int)response.StatusCode, "Error calling CreateRefund: " + response.Content, response.Content);
      }
      return (CreateRefundOut) this.apiClient.Deserialize(response.Content, typeof(CreateRefundOut));
    }
	
	 /// <summary>
    /// Create a refund 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>CreateRefundOut</returns>
    public async Task<CreateRefundOut> CreateRefundAsync (string Key, CreateRefundIn Input) {

      
          // verify the required parameter 'Key' is set
          if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling CreateRefund");
      
          // verify the required parameter 'Input' is set
          if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling CreateRefund");
      

      var path = "/api/v1/transactions/{key}/refunds";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "key" + "}", this.apiClient.ParameterToString(Key));
      

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
        throw new ApiException ((int)response.StatusCode, "Error calling CreateRefund: " + response.Content, response.Content);
      }
      return (CreateRefundOut) this.apiClient.Deserialize(response.Content, typeof(CreateRefundOut));
    }
    
  }  
  
}
