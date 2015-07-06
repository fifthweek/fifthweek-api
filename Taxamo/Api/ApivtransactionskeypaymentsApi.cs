namespace Taxamo.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestSharp;

    using Taxamo.Client;
    using Taxamo.Model;

    public interface IApivtransactionskeypaymentsApi {
    
    /// <summary>
    /// List payments 
    /// </summary>
    /// <param name="Limit">Max record count (no more than 100, defaults to 10).</param>/// <param name="Offset">How many records need to be skipped, defaults to 0.</param>/// <param name="Key">Transaction key.</param>
    /// <returns>ListPaymentsOut</returns>
    ListPaymentsOut ListPayments (string Limit, string Offset, string Key);

    /// <summary>
    /// List payments 
    /// </summary>
    /// <param name="Limit">Max record count (no more than 100, defaults to 10).</param>/// <param name="Offset">How many records need to be skipped, defaults to 0.</param>/// <param name="Key">Transaction key.</param>
    /// <returns>ListPaymentsOut</returns>
    Task<ListPaymentsOut> ListPaymentsAsync (string Limit, string Offset, string Key);
    
    /// <summary>
    /// Register a payment 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>CreatePaymentOut</returns>
    CreatePaymentOut CreatePayment (string Key, CreatePaymentIn Input);

    /// <summary>
    /// Register a payment 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>CreatePaymentOut</returns>
    Task<CreatePaymentOut> CreatePaymentAsync (string Key, CreatePaymentIn Input);
    
    /// <summary>
    /// Capture payment 
    /// </summary>
    /// <param name="Key">Transaction key.</param>
    /// <returns>CapturePaymentOut</returns>
    CapturePaymentOut CapturePayment (string Key);

    /// <summary>
    /// Capture payment 
    /// </summary>
    /// <param name="Key">Transaction key.</param>
    /// <returns>CapturePaymentOut</returns>
    Task<CapturePaymentOut> CapturePaymentAsync (string Key);
    
  }

  /// <summary>
  /// Represents a collection of functions to interact with the API endpoints
  /// </summary>
  public class ApivtransactionskeypaymentsApi : IApivtransactionskeypaymentsApi {

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivtransactionskeypaymentsApi"/> class.
    /// </summary>
    /// <param name="apiClient"> an instance of ApiClient (optional)
    /// <returns></returns>
    public ApivtransactionskeypaymentsApi(ApiClient apiClient = null) {
      if (apiClient == null) { // use the default one in Configuration
        this.apiClient = Configuration.apiClient; 
      } else {
        this.apiClient = apiClient;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivtransactionskeypaymentsApi"/> class.
    /// </summary>
    /// <returns></returns>
    public ApivtransactionskeypaymentsApi(String basePath)
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
    /// List payments 
    /// </summary>
    /// <param name="Limit">Max record count (no more than 100, defaults to 10).</param>/// <param name="Offset">How many records need to be skipped, defaults to 0.</param>/// <param name="Key">Transaction key.</param>
    /// <returns>ListPaymentsOut</returns>
    public ListPaymentsOut ListPayments (string Limit, string Offset, string Key) {

      
      // verify the required parameter 'Key' is set
      if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling ListPayments");
      

      var path = "/api/v1/transactions/{key}/payments";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "key" + "}", this.apiClient.ParameterToString(Key));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (Limit != null) queryParams.Add("limit", this.apiClient.ParameterToString(Limit)); // query parameter
       if (Offset != null) queryParams.Add("offset", this.apiClient.ParameterToString(Offset)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling ListPayments: " + response.Content, response.Content);
      }
      return (ListPaymentsOut) this.apiClient.Deserialize(response.Content, typeof(ListPaymentsOut));
    }
	
	 /// <summary>
    /// List payments 
    /// </summary>
    /// <param name="Limit">Max record count (no more than 100, defaults to 10).</param>/// <param name="Offset">How many records need to be skipped, defaults to 0.</param>/// <param name="Key">Transaction key.</param>
    /// <returns>ListPaymentsOut</returns>
    public async Task<ListPaymentsOut> ListPaymentsAsync (string Limit, string Offset, string Key) {

      
          // verify the required parameter 'Key' is set
          if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling ListPayments");
      

      var path = "/api/v1/transactions/{key}/payments";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "key" + "}", this.apiClient.ParameterToString(Key));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (Limit != null) queryParams.Add("limit", this.apiClient.ParameterToString(Limit)); // query parameter
       if (Offset != null) queryParams.Add("offset", this.apiClient.ParameterToString(Offset)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling ListPayments: " + response.Content, response.Content);
      }
      return (ListPaymentsOut) this.apiClient.Deserialize(response.Content, typeof(ListPaymentsOut));
    }
    
    /// <summary>
    /// Register a payment 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>CreatePaymentOut</returns>
    public CreatePaymentOut CreatePayment (string Key, CreatePaymentIn Input) {

      
      // verify the required parameter 'Key' is set
      if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling CreatePayment");
      
      // verify the required parameter 'Input' is set
      if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling CreatePayment");
      

      var path = "/api/v1/transactions/{key}/payments";
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
        throw new ApiException ((int)response.StatusCode, "Error calling CreatePayment: " + response.Content, response.Content);
      }
      return (CreatePaymentOut) this.apiClient.Deserialize(response.Content, typeof(CreatePaymentOut));
    }
	
	 /// <summary>
    /// Register a payment 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>CreatePaymentOut</returns>
    public async Task<CreatePaymentOut> CreatePaymentAsync (string Key, CreatePaymentIn Input) {

      
          // verify the required parameter 'Key' is set
          if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling CreatePayment");
      
          // verify the required parameter 'Input' is set
          if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling CreatePayment");
      

      var path = "/api/v1/transactions/{key}/payments";
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
        throw new ApiException ((int)response.StatusCode, "Error calling CreatePayment: " + response.Content, response.Content);
      }
      return (CreatePaymentOut) this.apiClient.Deserialize(response.Content, typeof(CreatePaymentOut));
    }
    
    /// <summary>
    /// Capture payment 
    /// </summary>
    /// <param name="Key">Transaction key.</param>
    /// <returns>CapturePaymentOut</returns>
    public CapturePaymentOut CapturePayment (string Key) {

      
      // verify the required parameter 'Key' is set
      if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling CapturePayment");
      

      var path = "/api/v1/transactions/{key}/payments/capture";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "key" + "}", this.apiClient.ParameterToString(Key));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling CapturePayment: " + response.Content, response.Content);
      }
      return (CapturePaymentOut) this.apiClient.Deserialize(response.Content, typeof(CapturePaymentOut));
    }
	
	 /// <summary>
    /// Capture payment 
    /// </summary>
    /// <param name="Key">Transaction key.</param>
    /// <returns>CapturePaymentOut</returns>
    public async Task<CapturePaymentOut> CapturePaymentAsync (string Key) {

      
          // verify the required parameter 'Key' is set
          if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling CapturePayment");
      

      var path = "/api/v1/transactions/{key}/payments/capture";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "key" + "}", this.apiClient.ParameterToString(Key));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling CapturePayment: " + response.Content, response.Content);
      }
      return (CapturePaymentOut) this.apiClient.Deserialize(response.Content, typeof(CapturePaymentOut));
    }
    
  }  
  
}
