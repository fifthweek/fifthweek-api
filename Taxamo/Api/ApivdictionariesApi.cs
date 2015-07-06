namespace Taxamo.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestSharp;

    using Taxamo.Client;
    using Taxamo.Model;

    public interface IApivdictionariesApi {
    
    /// <summary>
    /// Countries 
    /// </summary>
    /// <param name="TaxSupported">Should only countries with tax supported be listed?</param>
    /// <returns>GetCountriesDictOut</returns>
    GetCountriesDictOut GetCountriesDict (bool? TaxSupported);

    /// <summary>
    /// Countries 
    /// </summary>
    /// <param name="TaxSupported">Should only countries with tax supported be listed?</param>
    /// <returns>GetCountriesDictOut</returns>
    Task<GetCountriesDictOut> GetCountriesDictAsync (bool? TaxSupported);
    
    /// <summary>
    /// Currencies 
    /// </summary>
    
    /// <returns>GetCurrenciesDictOut</returns>
    GetCurrenciesDictOut GetCurrenciesDict ();

    /// <summary>
    /// Currencies 
    /// </summary>
    
    /// <returns>GetCurrenciesDictOut</returns>
    Task<GetCurrenciesDictOut> GetCurrenciesDictAsync ();
    
    /// <summary>
    /// Product types 
    /// </summary>
    
    /// <returns>GetProductTypesDictOut</returns>
    GetProductTypesDictOut GetProductTypesDict ();

    /// <summary>
    /// Product types 
    /// </summary>
    
    /// <returns>GetProductTypesDictOut</returns>
    Task<GetProductTypesDictOut> GetProductTypesDictAsync ();
    
  }

  /// <summary>
  /// Represents a collection of functions to interact with the API endpoints
  /// </summary>
  public class ApivdictionariesApi : IApivdictionariesApi {

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivdictionariesApi"/> class.
    /// </summary>
    /// <param name="apiClient"> an instance of ApiClient (optional)
    /// <returns></returns>
    public ApivdictionariesApi(ApiClient apiClient = null) {
      if (apiClient == null) { // use the default one in Configuration
        this.apiClient = Configuration.apiClient; 
      } else {
        this.apiClient = apiClient;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivdictionariesApi"/> class.
    /// </summary>
    /// <returns></returns>
    public ApivdictionariesApi(String basePath)
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
    /// Countries 
    /// </summary>
    /// <param name="TaxSupported">Should only countries with tax supported be listed?</param>
    /// <returns>GetCountriesDictOut</returns>
    public GetCountriesDictOut GetCountriesDict (bool? TaxSupported) {

      

      var path = "/api/v1/dictionaries/countries";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (TaxSupported != null) queryParams.Add("tax_supported", this.apiClient.ParameterToString(TaxSupported)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetCountriesDict: " + response.Content, response.Content);
      }
      return (GetCountriesDictOut) this.apiClient.Deserialize(response.Content, typeof(GetCountriesDictOut));
    }
	
	 /// <summary>
    /// Countries 
    /// </summary>
    /// <param name="TaxSupported">Should only countries with tax supported be listed?</param>
    /// <returns>GetCountriesDictOut</returns>
    public async Task<GetCountriesDictOut> GetCountriesDictAsync (bool? TaxSupported) {

      

      var path = "/api/v1/dictionaries/countries";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (TaxSupported != null) queryParams.Add("tax_supported", this.apiClient.ParameterToString(TaxSupported)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetCountriesDict: " + response.Content, response.Content);
      }
      return (GetCountriesDictOut) this.apiClient.Deserialize(response.Content, typeof(GetCountriesDictOut));
    }
    
    /// <summary>
    /// Currencies 
    /// </summary>
    
    /// <returns>GetCurrenciesDictOut</returns>
    public GetCurrenciesDictOut GetCurrenciesDict () {

      

      var path = "/api/v1/dictionaries/currencies";
      path = path.Replace("{format}", "json");
      

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
        throw new ApiException ((int)response.StatusCode, "Error calling GetCurrenciesDict: " + response.Content, response.Content);
      }
      return (GetCurrenciesDictOut) this.apiClient.Deserialize(response.Content, typeof(GetCurrenciesDictOut));
    }
	
	 /// <summary>
    /// Currencies 
    /// </summary>
    
    /// <returns>GetCurrenciesDictOut</returns>
    public async Task<GetCurrenciesDictOut> GetCurrenciesDictAsync () {

      

      var path = "/api/v1/dictionaries/currencies";
      path = path.Replace("{format}", "json");
      

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
        throw new ApiException ((int)response.StatusCode, "Error calling GetCurrenciesDict: " + response.Content, response.Content);
      }
      return (GetCurrenciesDictOut) this.apiClient.Deserialize(response.Content, typeof(GetCurrenciesDictOut));
    }
    
    /// <summary>
    /// Product types 
    /// </summary>
    
    /// <returns>GetProductTypesDictOut</returns>
    public GetProductTypesDictOut GetProductTypesDict () {

      

      var path = "/api/v1/dictionaries/product_types";
      path = path.Replace("{format}", "json");
      

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
        throw new ApiException ((int)response.StatusCode, "Error calling GetProductTypesDict: " + response.Content, response.Content);
      }
      return (GetProductTypesDictOut) this.apiClient.Deserialize(response.Content, typeof(GetProductTypesDictOut));
    }
	
	 /// <summary>
    /// Product types 
    /// </summary>
    
    /// <returns>GetProductTypesDictOut</returns>
    public async Task<GetProductTypesDictOut> GetProductTypesDictAsync () {

      

      var path = "/api/v1/dictionaries/product_types";
      path = path.Replace("{format}", "json");
      

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
        throw new ApiException ((int)response.StatusCode, "Error calling GetProductTypesDict: " + response.Content, response.Content);
      }
      return (GetProductTypesDictOut) this.apiClient.Deserialize(response.Content, typeof(GetProductTypesDictOut));
    }
    
  }  
  
}
