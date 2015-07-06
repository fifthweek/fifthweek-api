namespace Taxamo.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestSharp;

    using Taxamo.Client;
    using Taxamo.Model;

    public interface IApivtransactionskeyinvoiceApi {
    
    /// <summary>
    /// Email invoice 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>EmailInvoiceOut</returns>
    EmailInvoiceOut EmailInvoice (string Key, EmailInvoiceIn Input);

    /// <summary>
    /// Email invoice 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>EmailInvoiceOut</returns>
    Task<EmailInvoiceOut> EmailInvoiceAsync (string Key, EmailInvoiceIn Input);
    
  }

  /// <summary>
  /// Represents a collection of functions to interact with the API endpoints
  /// </summary>
  public class ApivtransactionskeyinvoiceApi : IApivtransactionskeyinvoiceApi {

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivtransactionskeyinvoiceApi"/> class.
    /// </summary>
    /// <param name="apiClient"> an instance of ApiClient (optional)
    /// <returns></returns>
    public ApivtransactionskeyinvoiceApi(ApiClient apiClient = null) {
      if (apiClient == null) { // use the default one in Configuration
        this.apiClient = Configuration.apiClient; 
      } else {
        this.apiClient = apiClient;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivtransactionskeyinvoiceApi"/> class.
    /// </summary>
    /// <returns></returns>
    public ApivtransactionskeyinvoiceApi(String basePath)
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
    /// Email invoice 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>EmailInvoiceOut</returns>
    public EmailInvoiceOut EmailInvoice (string Key, EmailInvoiceIn Input) {

      
      // verify the required parameter 'Key' is set
      if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling EmailInvoice");
      
      // verify the required parameter 'Input' is set
      if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling EmailInvoice");
      

      var path = "/api/v1/transactions/{key}/invoice/send_email";
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
        throw new ApiException ((int)response.StatusCode, "Error calling EmailInvoice: " + response.Content, response.Content);
      }
      return (EmailInvoiceOut) this.apiClient.Deserialize(response.Content, typeof(EmailInvoiceOut));
    }
	
	 /// <summary>
    /// Email invoice 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>EmailInvoiceOut</returns>
    public async Task<EmailInvoiceOut> EmailInvoiceAsync (string Key, EmailInvoiceIn Input) {

      
          // verify the required parameter 'Key' is set
          if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling EmailInvoice");
      
          // verify the required parameter 'Input' is set
          if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling EmailInvoice");
      

      var path = "/api/v1/transactions/{key}/invoice/send_email";
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
        throw new ApiException ((int)response.StatusCode, "Error calling EmailInvoice: " + response.Content, response.Content);
      }
      return (EmailInvoiceOut) this.apiClient.Deserialize(response.Content, typeof(EmailInvoiceOut));
    }
    
  }  
  
}
