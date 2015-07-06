namespace Taxamo.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestSharp;

    using Taxamo.Client;
    using Taxamo.Model;

    public interface IApivsettlementApi {
    
    /// <summary>
    /// Fetch refunds 
    /// </summary>
    /// <param name="Format">Output format. &#39;csv&#39; value is accepted as well</param>/// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="DateFrom">Take only refunds issued at or after the date. Format: yyyy-MM-dd</param>
    /// <returns>GetRefundsOut</returns>
    GetRefundsOut GetRefunds (string Format, string MossCountryCode, string DateFrom);

    /// <summary>
    /// Fetch refunds 
    /// </summary>
    /// <param name="Format">Output format. &#39;csv&#39; value is accepted as well</param>/// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="DateFrom">Take only refunds issued at or after the date. Format: yyyy-MM-dd</param>
    /// <returns>GetRefundsOut</returns>
    Task<GetRefundsOut> GetRefundsAsync (string Format, string MossCountryCode, string DateFrom);
    
    /// <summary>
    /// Fetch summary 
    /// </summary>
    /// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="Quarter">Quarter in yyyy-MM format.</param>
    /// <returns>GetSettlementSummaryOut</returns>
    GetSettlementSummaryOut GetSettlementSummary (string MossCountryCode, string Quarter);

    /// <summary>
    /// Fetch summary 
    /// </summary>
    /// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="Quarter">Quarter in yyyy-MM format.</param>
    /// <returns>GetSettlementSummaryOut</returns>
    Task<GetSettlementSummaryOut> GetSettlementSummaryAsync (string MossCountryCode, string Quarter);
    
    /// <summary>
    /// Fetch settlement 
    /// </summary>
    /// <param name="Format">Output format. &#39;csv&#39; value is accepted as well</param>/// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="MossTaxId">MOSS-assigned tax ID - if not provided, merchant&#39;s national tax number will be used.</param>/// <param name="Quarter">Quarter in yyyy-MM format.</param>
    /// <returns>GetSettlementOut</returns>
    GetSettlementOut GetSettlement (string Format, string MossCountryCode, string MossTaxId, string Quarter);

    /// <summary>
    /// Fetch settlement 
    /// </summary>
    /// <param name="Format">Output format. &#39;csv&#39; value is accepted as well</param>/// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="MossTaxId">MOSS-assigned tax ID - if not provided, merchant&#39;s national tax number will be used.</param>/// <param name="Quarter">Quarter in yyyy-MM format.</param>
    /// <returns>GetSettlementOut</returns>
    Task<GetSettlementOut> GetSettlementAsync (string Format, string MossCountryCode, string MossTaxId, string Quarter);
    
  }

  /// <summary>
  /// Represents a collection of functions to interact with the API endpoints
  /// </summary>
  public class ApivsettlementApi : IApivsettlementApi {

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivsettlementApi"/> class.
    /// </summary>
    /// <param name="apiClient"> an instance of ApiClient (optional)
    /// <returns></returns>
    public ApivsettlementApi(ApiClient apiClient = null) {
      if (apiClient == null) { // use the default one in Configuration
        this.apiClient = Configuration.apiClient; 
      } else {
        this.apiClient = apiClient;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivsettlementApi"/> class.
    /// </summary>
    /// <returns></returns>
    public ApivsettlementApi(String basePath)
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
    /// Fetch refunds 
    /// </summary>
    /// <param name="Format">Output format. &#39;csv&#39; value is accepted as well</param>/// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="DateFrom">Take only refunds issued at or after the date. Format: yyyy-MM-dd</param>
    /// <returns>GetRefundsOut</returns>
    public GetRefundsOut GetRefunds (string Format, string MossCountryCode, string DateFrom) {

      
      // verify the required parameter 'DateFrom' is set
      if (DateFrom == null) throw new ApiException(400, "Missing required parameter 'DateFrom' when calling GetRefunds");
      

      var path = "/api/v1/settlement/refunds";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (Format != null) queryParams.Add("format", this.apiClient.ParameterToString(Format)); // query parameter
       if (MossCountryCode != null) queryParams.Add("moss_country_code", this.apiClient.ParameterToString(MossCountryCode)); // query parameter
       if (DateFrom != null) queryParams.Add("date_from", this.apiClient.ParameterToString(DateFrom)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetRefunds: " + response.Content, response.Content);
      }
      return (GetRefundsOut) this.apiClient.Deserialize(response.Content, typeof(GetRefundsOut));
    }
	
	 /// <summary>
    /// Fetch refunds 
    /// </summary>
    /// <param name="Format">Output format. &#39;csv&#39; value is accepted as well</param>/// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="DateFrom">Take only refunds issued at or after the date. Format: yyyy-MM-dd</param>
    /// <returns>GetRefundsOut</returns>
    public async Task<GetRefundsOut> GetRefundsAsync (string Format, string MossCountryCode, string DateFrom) {

      
          // verify the required parameter 'DateFrom' is set
          if (DateFrom == null) throw new ApiException(400, "Missing required parameter 'DateFrom' when calling GetRefunds");
      

      var path = "/api/v1/settlement/refunds";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (Format != null) queryParams.Add("format", this.apiClient.ParameterToString(Format)); // query parameter
       if (MossCountryCode != null) queryParams.Add("moss_country_code", this.apiClient.ParameterToString(MossCountryCode)); // query parameter
       if (DateFrom != null) queryParams.Add("date_from", this.apiClient.ParameterToString(DateFrom)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetRefunds: " + response.Content, response.Content);
      }
      return (GetRefundsOut) this.apiClient.Deserialize(response.Content, typeof(GetRefundsOut));
    }
    
    /// <summary>
    /// Fetch summary 
    /// </summary>
    /// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="Quarter">Quarter in yyyy-MM format.</param>
    /// <returns>GetSettlementSummaryOut</returns>
    public GetSettlementSummaryOut GetSettlementSummary (string MossCountryCode, string Quarter) {

      
      // verify the required parameter 'Quarter' is set
      if (Quarter == null) throw new ApiException(400, "Missing required parameter 'Quarter' when calling GetSettlementSummary");
      

      var path = "/api/v1/settlement/summary/{quarter}";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "quarter" + "}", this.apiClient.ParameterToString(Quarter));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (MossCountryCode != null) queryParams.Add("moss_country_code", this.apiClient.ParameterToString(MossCountryCode)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetSettlementSummary: " + response.Content, response.Content);
      }
      return (GetSettlementSummaryOut) this.apiClient.Deserialize(response.Content, typeof(GetSettlementSummaryOut));
    }
	
	 /// <summary>
    /// Fetch summary 
    /// </summary>
    /// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="Quarter">Quarter in yyyy-MM format.</param>
    /// <returns>GetSettlementSummaryOut</returns>
    public async Task<GetSettlementSummaryOut> GetSettlementSummaryAsync (string MossCountryCode, string Quarter) {

      
          // verify the required parameter 'Quarter' is set
          if (Quarter == null) throw new ApiException(400, "Missing required parameter 'Quarter' when calling GetSettlementSummary");
      

      var path = "/api/v1/settlement/summary/{quarter}";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "quarter" + "}", this.apiClient.ParameterToString(Quarter));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (MossCountryCode != null) queryParams.Add("moss_country_code", this.apiClient.ParameterToString(MossCountryCode)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetSettlementSummary: " + response.Content, response.Content);
      }
      return (GetSettlementSummaryOut) this.apiClient.Deserialize(response.Content, typeof(GetSettlementSummaryOut));
    }
    
    /// <summary>
    /// Fetch settlement 
    /// </summary>
    /// <param name="Format">Output format. &#39;csv&#39; value is accepted as well</param>/// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="MossTaxId">MOSS-assigned tax ID - if not provided, merchant&#39;s national tax number will be used.</param>/// <param name="Quarter">Quarter in yyyy-MM format.</param>
    /// <returns>GetSettlementOut</returns>
    public GetSettlementOut GetSettlement (string Format, string MossCountryCode, string MossTaxId, string Quarter) {

      
      // verify the required parameter 'Quarter' is set
      if (Quarter == null) throw new ApiException(400, "Missing required parameter 'Quarter' when calling GetSettlement");
      

      var path = "/api/v1/settlement/{quarter}";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "quarter" + "}", this.apiClient.ParameterToString(Quarter));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (Format != null) queryParams.Add("format", this.apiClient.ParameterToString(Format)); // query parameter
       if (MossCountryCode != null) queryParams.Add("moss_country_code", this.apiClient.ParameterToString(MossCountryCode)); // query parameter
       if (MossTaxId != null) queryParams.Add("moss_tax_id", this.apiClient.ParameterToString(MossTaxId)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetSettlement: " + response.Content, response.Content);
      }
      return (GetSettlementOut) this.apiClient.Deserialize(response.Content, typeof(GetSettlementOut));
    }
	
	 /// <summary>
    /// Fetch settlement 
    /// </summary>
    /// <param name="Format">Output format. &#39;csv&#39; value is accepted as well</param>/// <param name="MossCountryCode">MOSS country code, used to determine currency. If ommited, merchant default setting is used.</param>/// <param name="MossTaxId">MOSS-assigned tax ID - if not provided, merchant&#39;s national tax number will be used.</param>/// <param name="Quarter">Quarter in yyyy-MM format.</param>
    /// <returns>GetSettlementOut</returns>
    public async Task<GetSettlementOut> GetSettlementAsync (string Format, string MossCountryCode, string MossTaxId, string Quarter) {

      
          // verify the required parameter 'Quarter' is set
          if (Quarter == null) throw new ApiException(400, "Missing required parameter 'Quarter' when calling GetSettlement");
      

      var path = "/api/v1/settlement/{quarter}";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "quarter" + "}", this.apiClient.ParameterToString(Quarter));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (Format != null) queryParams.Add("format", this.apiClient.ParameterToString(Format)); // query parameter
       if (MossCountryCode != null) queryParams.Add("moss_country_code", this.apiClient.ParameterToString(MossCountryCode)); // query parameter
       if (MossTaxId != null) queryParams.Add("moss_tax_id", this.apiClient.ParameterToString(MossTaxId)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetSettlement: " + response.Content, response.Content);
      }
      return (GetSettlementOut) this.apiClient.Deserialize(response.Content, typeof(GetSettlementOut));
    }
    
  }  
  
}
