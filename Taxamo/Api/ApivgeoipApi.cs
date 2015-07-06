namespace Taxamo.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestSharp;

    using Taxamo.Client;
    using Taxamo.Model;

    public interface IApivgeoipApi {
    
    /// <summary>
    /// Locate IP 
    /// </summary>
    
    /// <returns>LocateMyIPOut</returns>
    LocateMyIPOut LocateMyIP ();

    /// <summary>
    /// Locate IP 
    /// </summary>
    
    /// <returns>LocateMyIPOut</returns>
    Task<LocateMyIPOut> LocateMyIPAsync ();
    
    /// <summary>
    /// Locate provided IP 
    /// </summary>
    /// <param name="Ip">IP address.</param>
    /// <returns>LocateGivenIPOut</returns>
    LocateGivenIPOut LocateGivenIP (string Ip);

    /// <summary>
    /// Locate provided IP 
    /// </summary>
    /// <param name="Ip">IP address.</param>
    /// <returns>LocateGivenIPOut</returns>
    Task<LocateGivenIPOut> LocateGivenIPAsync (string Ip);
    
  }

  /// <summary>
  /// Represents a collection of functions to interact with the API endpoints
  /// </summary>
  public class ApivgeoipApi : IApivgeoipApi {

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivgeoipApi"/> class.
    /// </summary>
    /// <param name="apiClient"> an instance of ApiClient (optional)
    /// <returns></returns>
    public ApivgeoipApi(ApiClient apiClient = null) {
      if (apiClient == null) { // use the default one in Configuration
        this.apiClient = Configuration.apiClient; 
      } else {
        this.apiClient = apiClient;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivgeoipApi"/> class.
    /// </summary>
    /// <returns></returns>
    public ApivgeoipApi(String basePath)
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
    /// Locate IP 
    /// </summary>
    
    /// <returns>LocateMyIPOut</returns>
    public LocateMyIPOut LocateMyIP () {

      

      var path = "/api/v1/geoip";
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
        throw new ApiException ((int)response.StatusCode, "Error calling LocateMyIP: " + response.Content, response.Content);
      }
      return (LocateMyIPOut) this.apiClient.Deserialize(response.Content, typeof(LocateMyIPOut));
    }
	
	 /// <summary>
    /// Locate IP 
    /// </summary>
    
    /// <returns>LocateMyIPOut</returns>
    public async Task<LocateMyIPOut> LocateMyIPAsync () {

      

      var path = "/api/v1/geoip";
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
        throw new ApiException ((int)response.StatusCode, "Error calling LocateMyIP: " + response.Content, response.Content);
      }
      return (LocateMyIPOut) this.apiClient.Deserialize(response.Content, typeof(LocateMyIPOut));
    }
    
    /// <summary>
    /// Locate provided IP 
    /// </summary>
    /// <param name="Ip">IP address.</param>
    /// <returns>LocateGivenIPOut</returns>
    public LocateGivenIPOut LocateGivenIP (string Ip) {

      
      // verify the required parameter 'Ip' is set
      if (Ip == null) throw new ApiException(400, "Missing required parameter 'Ip' when calling LocateGivenIP");
      

      var path = "/api/v1/geoip/{ip}";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "ip" + "}", this.apiClient.ParameterToString(Ip));
      

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
        throw new ApiException ((int)response.StatusCode, "Error calling LocateGivenIP: " + response.Content, response.Content);
      }
      return (LocateGivenIPOut) this.apiClient.Deserialize(response.Content, typeof(LocateGivenIPOut));
    }
	
	 /// <summary>
    /// Locate provided IP 
    /// </summary>
    /// <param name="Ip">IP address.</param>
    /// <returns>LocateGivenIPOut</returns>
    public async Task<LocateGivenIPOut> LocateGivenIPAsync (string Ip) {

      
          // verify the required parameter 'Ip' is set
          if (Ip == null) throw new ApiException(400, "Missing required parameter 'Ip' when calling LocateGivenIP");
      

      var path = "/api/v1/geoip/{ip}";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "ip" + "}", this.apiClient.ParameterToString(Ip));
      

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
        throw new ApiException ((int)response.StatusCode, "Error calling LocateGivenIP: " + response.Content, response.Content);
      }
      return (LocateGivenIPOut) this.apiClient.Deserialize(response.Content, typeof(LocateGivenIPOut));
    }
    
  }  
  
}
