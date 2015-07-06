namespace Taxamo.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestSharp;

    using Taxamo.Client;
    using Taxamo.Model;

    public interface IApivstatsApi {
    
    /// <summary>
    /// Settlement by country 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetSettlementStatsByCountryOut</returns>
    GetSettlementStatsByCountryOut GetSettlementStatsByCountry (string DateFrom, string DateTo);

    /// <summary>
    /// Settlement by country 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetSettlementStatsByCountryOut</returns>
    Task<GetSettlementStatsByCountryOut> GetSettlementStatsByCountryAsync (string DateFrom, string DateTo);
    
    /// <summary>
    /// Settlement by tax type 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetSettlementStatsByTaxationTypeOut</returns>
    GetSettlementStatsByTaxationTypeOut GetSettlementStatsByTaxationType (string DateFrom, string DateTo);

    /// <summary>
    /// Settlement by tax type 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetSettlementStatsByTaxationTypeOut</returns>
    Task<GetSettlementStatsByTaxationTypeOut> GetSettlementStatsByTaxationTypeAsync (string DateFrom, string DateTo);
    
    /// <summary>
    /// Settlement stats over time 
    /// </summary>
    /// <param name="Interval">Interval type - day, week, month.</param>/// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetDailySettlementStatsOut</returns>
    GetDailySettlementStatsOut GetDailySettlementStats (string Interval, string DateFrom, string DateTo);

    /// <summary>
    /// Settlement stats over time 
    /// </summary>
    /// <param name="Interval">Interval type - day, week, month.</param>/// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetDailySettlementStatsOut</returns>
    Task<GetDailySettlementStatsOut> GetDailySettlementStatsAsync (string Interval, string DateFrom, string DateTo);
    
    /// <summary>
    /// Transaction stats 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>/// <param name="Interval">Interval. Accepted values are &#39;day&#39;, &#39;week&#39; and &#39;month&#39;.</param>
    /// <returns>GetTransactionsStatsOut</returns>
    GetTransactionsStatsOut GetTransactionsStats (string DateFrom, string DateTo, string Interval);

    /// <summary>
    /// Transaction stats 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>/// <param name="Interval">Interval. Accepted values are &#39;day&#39;, &#39;week&#39; and &#39;month&#39;.</param>
    /// <returns>GetTransactionsStatsOut</returns>
    Task<GetTransactionsStatsOut> GetTransactionsStatsAsync (string DateFrom, string DateTo, string Interval);
    
  }

  /// <summary>
  /// Represents a collection of functions to interact with the API endpoints
  /// </summary>
  public class ApivstatsApi : IApivstatsApi {

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivstatsApi"/> class.
    /// </summary>
    /// <param name="apiClient"> an instance of ApiClient (optional)
    /// <returns></returns>
    public ApivstatsApi(ApiClient apiClient = null) {
      if (apiClient == null) { // use the default one in Configuration
        this.apiClient = Configuration.apiClient; 
      } else {
        this.apiClient = apiClient;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivstatsApi"/> class.
    /// </summary>
    /// <returns></returns>
    public ApivstatsApi(String basePath)
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
    /// Settlement by country 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetSettlementStatsByCountryOut</returns>
    public GetSettlementStatsByCountryOut GetSettlementStatsByCountry (string DateFrom, string DateTo) {

      
      // verify the required parameter 'DateFrom' is set
      if (DateFrom == null) throw new ApiException(400, "Missing required parameter 'DateFrom' when calling GetSettlementStatsByCountry");
      
      // verify the required parameter 'DateTo' is set
      if (DateTo == null) throw new ApiException(400, "Missing required parameter 'DateTo' when calling GetSettlementStatsByCountry");
      

      var path = "/api/v1/stats/settlement/by_country";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (DateFrom != null) queryParams.Add("date_from", this.apiClient.ParameterToString(DateFrom)); // query parameter
       if (DateTo != null) queryParams.Add("date_to", this.apiClient.ParameterToString(DateTo)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetSettlementStatsByCountry: " + response.Content, response.Content);
      }
      return (GetSettlementStatsByCountryOut) this.apiClient.Deserialize(response.Content, typeof(GetSettlementStatsByCountryOut));
    }
	
	 /// <summary>
    /// Settlement by country 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetSettlementStatsByCountryOut</returns>
    public async Task<GetSettlementStatsByCountryOut> GetSettlementStatsByCountryAsync (string DateFrom, string DateTo) {

      
          // verify the required parameter 'DateFrom' is set
          if (DateFrom == null) throw new ApiException(400, "Missing required parameter 'DateFrom' when calling GetSettlementStatsByCountry");
      
          // verify the required parameter 'DateTo' is set
          if (DateTo == null) throw new ApiException(400, "Missing required parameter 'DateTo' when calling GetSettlementStatsByCountry");
      

      var path = "/api/v1/stats/settlement/by_country";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (DateFrom != null) queryParams.Add("date_from", this.apiClient.ParameterToString(DateFrom)); // query parameter
       if (DateTo != null) queryParams.Add("date_to", this.apiClient.ParameterToString(DateTo)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetSettlementStatsByCountry: " + response.Content, response.Content);
      }
      return (GetSettlementStatsByCountryOut) this.apiClient.Deserialize(response.Content, typeof(GetSettlementStatsByCountryOut));
    }
    
    /// <summary>
    /// Settlement by tax type 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetSettlementStatsByTaxationTypeOut</returns>
    public GetSettlementStatsByTaxationTypeOut GetSettlementStatsByTaxationType (string DateFrom, string DateTo) {

      
      // verify the required parameter 'DateFrom' is set
      if (DateFrom == null) throw new ApiException(400, "Missing required parameter 'DateFrom' when calling GetSettlementStatsByTaxationType");
      
      // verify the required parameter 'DateTo' is set
      if (DateTo == null) throw new ApiException(400, "Missing required parameter 'DateTo' when calling GetSettlementStatsByTaxationType");
      

      var path = "/api/v1/stats/settlement/by_taxation_type";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (DateFrom != null) queryParams.Add("date_from", this.apiClient.ParameterToString(DateFrom)); // query parameter
       if (DateTo != null) queryParams.Add("date_to", this.apiClient.ParameterToString(DateTo)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetSettlementStatsByTaxationType: " + response.Content, response.Content);
      }
      return (GetSettlementStatsByTaxationTypeOut) this.apiClient.Deserialize(response.Content, typeof(GetSettlementStatsByTaxationTypeOut));
    }
	
	 /// <summary>
    /// Settlement by tax type 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetSettlementStatsByTaxationTypeOut</returns>
    public async Task<GetSettlementStatsByTaxationTypeOut> GetSettlementStatsByTaxationTypeAsync (string DateFrom, string DateTo) {

      
          // verify the required parameter 'DateFrom' is set
          if (DateFrom == null) throw new ApiException(400, "Missing required parameter 'DateFrom' when calling GetSettlementStatsByTaxationType");
      
          // verify the required parameter 'DateTo' is set
          if (DateTo == null) throw new ApiException(400, "Missing required parameter 'DateTo' when calling GetSettlementStatsByTaxationType");
      

      var path = "/api/v1/stats/settlement/by_taxation_type";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (DateFrom != null) queryParams.Add("date_from", this.apiClient.ParameterToString(DateFrom)); // query parameter
       if (DateTo != null) queryParams.Add("date_to", this.apiClient.ParameterToString(DateTo)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetSettlementStatsByTaxationType: " + response.Content, response.Content);
      }
      return (GetSettlementStatsByTaxationTypeOut) this.apiClient.Deserialize(response.Content, typeof(GetSettlementStatsByTaxationTypeOut));
    }
    
    /// <summary>
    /// Settlement stats over time 
    /// </summary>
    /// <param name="Interval">Interval type - day, week, month.</param>/// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetDailySettlementStatsOut</returns>
    public GetDailySettlementStatsOut GetDailySettlementStats (string Interval, string DateFrom, string DateTo) {

      
      // verify the required parameter 'Interval' is set
      if (Interval == null) throw new ApiException(400, "Missing required parameter 'Interval' when calling GetDailySettlementStats");
      
      // verify the required parameter 'DateFrom' is set
      if (DateFrom == null) throw new ApiException(400, "Missing required parameter 'DateFrom' when calling GetDailySettlementStats");
      
      // verify the required parameter 'DateTo' is set
      if (DateTo == null) throw new ApiException(400, "Missing required parameter 'DateTo' when calling GetDailySettlementStats");
      

      var path = "/api/v1/stats/settlement/daily";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (Interval != null) queryParams.Add("interval", this.apiClient.ParameterToString(Interval)); // query parameter
       if (DateFrom != null) queryParams.Add("date_from", this.apiClient.ParameterToString(DateFrom)); // query parameter
       if (DateTo != null) queryParams.Add("date_to", this.apiClient.ParameterToString(DateTo)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetDailySettlementStats: " + response.Content, response.Content);
      }
      return (GetDailySettlementStatsOut) this.apiClient.Deserialize(response.Content, typeof(GetDailySettlementStatsOut));
    }
	
	 /// <summary>
    /// Settlement stats over time 
    /// </summary>
    /// <param name="Interval">Interval type - day, week, month.</param>/// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>
    /// <returns>GetDailySettlementStatsOut</returns>
    public async Task<GetDailySettlementStatsOut> GetDailySettlementStatsAsync (string Interval, string DateFrom, string DateTo) {

      
          // verify the required parameter 'Interval' is set
          if (Interval == null) throw new ApiException(400, "Missing required parameter 'Interval' when calling GetDailySettlementStats");
      
          // verify the required parameter 'DateFrom' is set
          if (DateFrom == null) throw new ApiException(400, "Missing required parameter 'DateFrom' when calling GetDailySettlementStats");
      
          // verify the required parameter 'DateTo' is set
          if (DateTo == null) throw new ApiException(400, "Missing required parameter 'DateTo' when calling GetDailySettlementStats");
      

      var path = "/api/v1/stats/settlement/daily";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (Interval != null) queryParams.Add("interval", this.apiClient.ParameterToString(Interval)); // query parameter
       if (DateFrom != null) queryParams.Add("date_from", this.apiClient.ParameterToString(DateFrom)); // query parameter
       if (DateTo != null) queryParams.Add("date_to", this.apiClient.ParameterToString(DateTo)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetDailySettlementStats: " + response.Content, response.Content);
      }
      return (GetDailySettlementStatsOut) this.apiClient.Deserialize(response.Content, typeof(GetDailySettlementStatsOut));
    }
    
    /// <summary>
    /// Transaction stats 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>/// <param name="Interval">Interval. Accepted values are &#39;day&#39;, &#39;week&#39; and &#39;month&#39;.</param>
    /// <returns>GetTransactionsStatsOut</returns>
    public GetTransactionsStatsOut GetTransactionsStats (string DateFrom, string DateTo, string Interval) {

      
      // verify the required parameter 'DateFrom' is set
      if (DateFrom == null) throw new ApiException(400, "Missing required parameter 'DateFrom' when calling GetTransactionsStats");
      
      // verify the required parameter 'DateTo' is set
      if (DateTo == null) throw new ApiException(400, "Missing required parameter 'DateTo' when calling GetTransactionsStats");
      

      var path = "/api/v1/stats/transactions";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (DateFrom != null) queryParams.Add("date_from", this.apiClient.ParameterToString(DateFrom)); // query parameter
       if (DateTo != null) queryParams.Add("date_to", this.apiClient.ParameterToString(DateTo)); // query parameter
       if (Interval != null) queryParams.Add("interval", this.apiClient.ParameterToString(Interval)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetTransactionsStats: " + response.Content, response.Content);
      }
      return (GetTransactionsStatsOut) this.apiClient.Deserialize(response.Content, typeof(GetTransactionsStatsOut));
    }
	
	 /// <summary>
    /// Transaction stats 
    /// </summary>
    /// <param name="DateFrom">Date from in yyyy-MM format.</param>/// <param name="DateTo">Date to in yyyy-MM format.</param>/// <param name="Interval">Interval. Accepted values are &#39;day&#39;, &#39;week&#39; and &#39;month&#39;.</param>
    /// <returns>GetTransactionsStatsOut</returns>
    public async Task<GetTransactionsStatsOut> GetTransactionsStatsAsync (string DateFrom, string DateTo, string Interval) {

      
          // verify the required parameter 'DateFrom' is set
          if (DateFrom == null) throw new ApiException(400, "Missing required parameter 'DateFrom' when calling GetTransactionsStats");
      
          // verify the required parameter 'DateTo' is set
          if (DateTo == null) throw new ApiException(400, "Missing required parameter 'DateTo' when calling GetTransactionsStats");
      

      var path = "/api/v1/stats/transactions";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (DateFrom != null) queryParams.Add("date_from", this.apiClient.ParameterToString(DateFrom)); // query parameter
       if (DateTo != null) queryParams.Add("date_to", this.apiClient.ParameterToString(DateTo)); // query parameter
       if (Interval != null) queryParams.Add("interval", this.apiClient.ParameterToString(Interval)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetTransactionsStats: " + response.Content, response.Content);
      }
      return (GetTransactionsStatsOut) this.apiClient.Deserialize(response.Content, typeof(GetTransactionsStatsOut));
    }
    
  }  
  
}
