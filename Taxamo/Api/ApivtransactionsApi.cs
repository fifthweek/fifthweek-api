namespace Taxamo.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestSharp;

    using Taxamo.Client;
    using Taxamo.Model;

    public interface IApivtransactionsApi {
    
    /// <summary>
    /// Browse transactions 
    /// </summary>
    /// <param name="FilterText">Filtering expression</param>/// <param name="Offset">Offset</param>/// <param name="KeyOrCustomId">Taxamo provided transaction key or custom id</param>/// <param name="CurrencyCode">Three letter ISO currency code.</param>/// <param name="OrderDateTo">Order date to in yyyy-MM-dd format.</param>/// <param name="SortReverse">If true, results are sorted in descending order.</param>/// <param name="Limit">Limit (no more than 1000, defaults to 100).</param>/// <param name="InvoiceNumber">Transaction invoice number.</param>/// <param name="Statuses">Comma separated list of of transaction statuses.</param>/// <param name="OrderDateFrom">Order date from in yyyy-MM-dd format.</param>/// <param name="Format">Output format - supports &#39;csv&#39; value for this operation.</param>/// <param name="TaxCountryCode">Two letter ISO tax country code.</param>
    /// <returns>ListTransactionsOut</returns>
    ListTransactionsOut ListTransactions (string FilterText, int? Offset, string KeyOrCustomId, string CurrencyCode, string OrderDateTo, bool? SortReverse, int? Limit, string InvoiceNumber, string Statuses, string OrderDateFrom, string Format, string TaxCountryCode);

    /// <summary>
    /// Browse transactions 
    /// </summary>
    /// <param name="FilterText">Filtering expression</param>/// <param name="Offset">Offset</param>/// <param name="KeyOrCustomId">Taxamo provided transaction key or custom id</param>/// <param name="CurrencyCode">Three letter ISO currency code.</param>/// <param name="OrderDateTo">Order date to in yyyy-MM-dd format.</param>/// <param name="SortReverse">If true, results are sorted in descending order.</param>/// <param name="Limit">Limit (no more than 1000, defaults to 100).</param>/// <param name="InvoiceNumber">Transaction invoice number.</param>/// <param name="Statuses">Comma separated list of of transaction statuses.</param>/// <param name="OrderDateFrom">Order date from in yyyy-MM-dd format.</param>/// <param name="Format">Output format - supports &#39;csv&#39; value for this operation.</param>/// <param name="TaxCountryCode">Two letter ISO tax country code.</param>
    /// <returns>ListTransactionsOut</returns>
    Task<ListTransactionsOut> ListTransactionsAsync (string FilterText, int? Offset, string KeyOrCustomId, string CurrencyCode, string OrderDateTo, bool? SortReverse, int? Limit, string InvoiceNumber, string Statuses, string OrderDateFrom, string Format, string TaxCountryCode);
    
    /// <summary>
    /// Store transaction 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CreateTransactionOut</returns>
    CreateTransactionOut CreateTransaction (CreateTransactionIn Input);

    /// <summary>
    /// Store transaction 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CreateTransactionOut</returns>
    Task<CreateTransactionOut> CreateTransactionAsync (CreateTransactionIn Input);
    
    /// <summary>
    /// Retrieve transaction data. 
    /// </summary>
    /// <param name="Key">Transaction key</param>
    /// <returns>GetTransactionOut</returns>
    GetTransactionOut GetTransaction (string Key);

    /// <summary>
    /// Retrieve transaction data. 
    /// </summary>
    /// <param name="Key">Transaction key</param>
    /// <returns>GetTransactionOut</returns>
    Task<GetTransactionOut> GetTransactionAsync (string Key);
    
    /// <summary>
    /// Update transaction 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>UpdateTransactionOut</returns>
    UpdateTransactionOut UpdateTransaction (string Key, UpdateTransactionIn Input);

    /// <summary>
    /// Update transaction 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>UpdateTransactionOut</returns>
    Task<UpdateTransactionOut> UpdateTransactionAsync (string Key, UpdateTransactionIn Input);
    
    /// <summary>
    /// Delete transaction 
    /// </summary>
    /// <param name="Key">Transaction key</param>
    /// <returns>CancelTransactionOut</returns>
    CancelTransactionOut CancelTransaction (string Key);

    /// <summary>
    /// Delete transaction 
    /// </summary>
    /// <param name="Key">Transaction key</param>
    /// <returns>CancelTransactionOut</returns>
    Task<CancelTransactionOut> CancelTransactionAsync (string Key);
    
    /// <summary>
    /// Confirm transaction 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>ConfirmTransactionOut</returns>
    ConfirmTransactionOut ConfirmTransaction (string Key, ConfirmTransactionIn Input);

    /// <summary>
    /// Confirm transaction 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>ConfirmTransactionOut</returns>
    Task<ConfirmTransactionOut> ConfirmTransactionAsync (string Key, ConfirmTransactionIn Input);
    
    /// <summary>
    /// Un-confirm the transaction. Un-confirmed transaction can be edited or canceled like a newly created one. 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>UnconfirmTransactionOut</returns>
    UnconfirmTransactionOut UnconfirmTransaction (string Key, UnconfirmTransactionIn Input);

    /// <summary>
    /// Un-confirm the transaction. Un-confirmed transaction can be edited or canceled like a newly created one. 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>UnconfirmTransactionOut</returns>
    Task<UnconfirmTransactionOut> UnconfirmTransactionAsync (string Key, UnconfirmTransactionIn Input);
    
  }

  /// <summary>
  /// Represents a collection of functions to interact with the API endpoints
  /// </summary>
  public class ApivtransactionsApi : IApivtransactionsApi {

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivtransactionsApi"/> class.
    /// </summary>
    /// <param name="apiClient"> an instance of ApiClient (optional)
    /// <returns></returns>
    public ApivtransactionsApi(ApiClient apiClient = null) {
      if (apiClient == null) { // use the default one in Configuration
        this.apiClient = Configuration.apiClient; 
      } else {
        this.apiClient = apiClient;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivtransactionsApi"/> class.
    /// </summary>
    /// <returns></returns>
    public ApivtransactionsApi(String basePath)
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
    /// Browse transactions 
    /// </summary>
    /// <param name="FilterText">Filtering expression</param>/// <param name="Offset">Offset</param>/// <param name="KeyOrCustomId">Taxamo provided transaction key or custom id</param>/// <param name="CurrencyCode">Three letter ISO currency code.</param>/// <param name="OrderDateTo">Order date to in yyyy-MM-dd format.</param>/// <param name="SortReverse">If true, results are sorted in descending order.</param>/// <param name="Limit">Limit (no more than 1000, defaults to 100).</param>/// <param name="InvoiceNumber">Transaction invoice number.</param>/// <param name="Statuses">Comma separated list of of transaction statuses.</param>/// <param name="OrderDateFrom">Order date from in yyyy-MM-dd format.</param>/// <param name="Format">Output format - supports &#39;csv&#39; value for this operation.</param>/// <param name="TaxCountryCode">Two letter ISO tax country code.</param>
    /// <returns>ListTransactionsOut</returns>
    public ListTransactionsOut ListTransactions (string FilterText, int? Offset, string KeyOrCustomId, string CurrencyCode, string OrderDateTo, bool? SortReverse, int? Limit, string InvoiceNumber, string Statuses, string OrderDateFrom, string Format, string TaxCountryCode) {

      

      var path = "/api/v1/transactions";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (FilterText != null) queryParams.Add("filter_text", this.apiClient.ParameterToString(FilterText)); // query parameter
       if (Offset != null) queryParams.Add("offset", this.apiClient.ParameterToString(Offset)); // query parameter
       if (KeyOrCustomId != null) queryParams.Add("key_or_custom_id", this.apiClient.ParameterToString(KeyOrCustomId)); // query parameter
       if (CurrencyCode != null) queryParams.Add("currency_code", this.apiClient.ParameterToString(CurrencyCode)); // query parameter
       if (OrderDateTo != null) queryParams.Add("order_date_to", this.apiClient.ParameterToString(OrderDateTo)); // query parameter
       if (SortReverse != null) queryParams.Add("sort_reverse", this.apiClient.ParameterToString(SortReverse)); // query parameter
       if (Limit != null) queryParams.Add("limit", this.apiClient.ParameterToString(Limit)); // query parameter
       if (InvoiceNumber != null) queryParams.Add("invoice_number", this.apiClient.ParameterToString(InvoiceNumber)); // query parameter
       if (Statuses != null) queryParams.Add("statuses", this.apiClient.ParameterToString(Statuses)); // query parameter
       if (OrderDateFrom != null) queryParams.Add("order_date_from", this.apiClient.ParameterToString(OrderDateFrom)); // query parameter
       if (Format != null) queryParams.Add("format", this.apiClient.ParameterToString(Format)); // query parameter
       if (TaxCountryCode != null) queryParams.Add("tax_country_code", this.apiClient.ParameterToString(TaxCountryCode)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling ListTransactions: " + response.Content, response.Content);
      }
      return (ListTransactionsOut) this.apiClient.Deserialize(response.Content, typeof(ListTransactionsOut));
    }
	
	 /// <summary>
    /// Browse transactions 
    /// </summary>
    /// <param name="FilterText">Filtering expression</param>/// <param name="Offset">Offset</param>/// <param name="KeyOrCustomId">Taxamo provided transaction key or custom id</param>/// <param name="CurrencyCode">Three letter ISO currency code.</param>/// <param name="OrderDateTo">Order date to in yyyy-MM-dd format.</param>/// <param name="SortReverse">If true, results are sorted in descending order.</param>/// <param name="Limit">Limit (no more than 1000, defaults to 100).</param>/// <param name="InvoiceNumber">Transaction invoice number.</param>/// <param name="Statuses">Comma separated list of of transaction statuses.</param>/// <param name="OrderDateFrom">Order date from in yyyy-MM-dd format.</param>/// <param name="Format">Output format - supports &#39;csv&#39; value for this operation.</param>/// <param name="TaxCountryCode">Two letter ISO tax country code.</param>
    /// <returns>ListTransactionsOut</returns>
    public async Task<ListTransactionsOut> ListTransactionsAsync (string FilterText, int? Offset, string KeyOrCustomId, string CurrencyCode, string OrderDateTo, bool? SortReverse, int? Limit, string InvoiceNumber, string Statuses, string OrderDateFrom, string Format, string TaxCountryCode) {

      

      var path = "/api/v1/transactions";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (FilterText != null) queryParams.Add("filter_text", this.apiClient.ParameterToString(FilterText)); // query parameter
       if (Offset != null) queryParams.Add("offset", this.apiClient.ParameterToString(Offset)); // query parameter
       if (KeyOrCustomId != null) queryParams.Add("key_or_custom_id", this.apiClient.ParameterToString(KeyOrCustomId)); // query parameter
       if (CurrencyCode != null) queryParams.Add("currency_code", this.apiClient.ParameterToString(CurrencyCode)); // query parameter
       if (OrderDateTo != null) queryParams.Add("order_date_to", this.apiClient.ParameterToString(OrderDateTo)); // query parameter
       if (SortReverse != null) queryParams.Add("sort_reverse", this.apiClient.ParameterToString(SortReverse)); // query parameter
       if (Limit != null) queryParams.Add("limit", this.apiClient.ParameterToString(Limit)); // query parameter
       if (InvoiceNumber != null) queryParams.Add("invoice_number", this.apiClient.ParameterToString(InvoiceNumber)); // query parameter
       if (Statuses != null) queryParams.Add("statuses", this.apiClient.ParameterToString(Statuses)); // query parameter
       if (OrderDateFrom != null) queryParams.Add("order_date_from", this.apiClient.ParameterToString(OrderDateFrom)); // query parameter
       if (Format != null) queryParams.Add("format", this.apiClient.ParameterToString(Format)); // query parameter
       if (TaxCountryCode != null) queryParams.Add("tax_country_code", this.apiClient.ParameterToString(TaxCountryCode)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling ListTransactions: " + response.Content, response.Content);
      }
      return (ListTransactionsOut) this.apiClient.Deserialize(response.Content, typeof(ListTransactionsOut));
    }
    
    /// <summary>
    /// Store transaction 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CreateTransactionOut</returns>
    public CreateTransactionOut CreateTransaction (CreateTransactionIn Input) {

      
      // verify the required parameter 'Input' is set
      if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling CreateTransaction");
      

      var path = "/api/v1/transactions";
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
        throw new ApiException ((int)response.StatusCode, "Error calling CreateTransaction: " + response.Content, response.Content);
      }
      return (CreateTransactionOut) this.apiClient.Deserialize(response.Content, typeof(CreateTransactionOut));
    }
	
	 /// <summary>
    /// Store transaction 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CreateTransactionOut</returns>
    public async Task<CreateTransactionOut> CreateTransactionAsync (CreateTransactionIn Input) {

      
          // verify the required parameter 'Input' is set
          if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling CreateTransaction");
      

      var path = "/api/v1/transactions";
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
        throw new ApiException ((int)response.StatusCode, "Error calling CreateTransaction: " + response.Content, response.Content);
      }
      return (CreateTransactionOut) this.apiClient.Deserialize(response.Content, typeof(CreateTransactionOut));
    }
    
    /// <summary>
    /// Retrieve transaction data. 
    /// </summary>
    /// <param name="Key">Transaction key</param>
    /// <returns>GetTransactionOut</returns>
    public GetTransactionOut GetTransaction (string Key) {

      
      // verify the required parameter 'Key' is set
      if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling GetTransaction");
      

      var path = "/api/v1/transactions/{key}";
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
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetTransaction: " + response.Content, response.Content);
      }
      return (GetTransactionOut) this.apiClient.Deserialize(response.Content, typeof(GetTransactionOut));
    }
	
	 /// <summary>
    /// Retrieve transaction data. 
    /// </summary>
    /// <param name="Key">Transaction key</param>
    /// <returns>GetTransactionOut</returns>
    public async Task<GetTransactionOut> GetTransactionAsync (string Key) {

      
          // verify the required parameter 'Key' is set
          if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling GetTransaction");
      

      var path = "/api/v1/transactions/{key}";
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
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling GetTransaction: " + response.Content, response.Content);
      }
      return (GetTransactionOut) this.apiClient.Deserialize(response.Content, typeof(GetTransactionOut));
    }
    
    /// <summary>
    /// Update transaction 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>UpdateTransactionOut</returns>
    public UpdateTransactionOut UpdateTransaction (string Key, UpdateTransactionIn Input) {

      
      // verify the required parameter 'Key' is set
      if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling UpdateTransaction");
      
      // verify the required parameter 'Input' is set
      if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling UpdateTransaction");
      

      var path = "/api/v1/transactions/{key}";
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
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling UpdateTransaction: " + response.Content, response.Content);
      }
      return (UpdateTransactionOut) this.apiClient.Deserialize(response.Content, typeof(UpdateTransactionOut));
    }
	
	 /// <summary>
    /// Update transaction 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>UpdateTransactionOut</returns>
    public async Task<UpdateTransactionOut> UpdateTransactionAsync (string Key, UpdateTransactionIn Input) {

      
          // verify the required parameter 'Key' is set
          if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling UpdateTransaction");
      
          // verify the required parameter 'Input' is set
          if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling UpdateTransaction");
      

      var path = "/api/v1/transactions/{key}";
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
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling UpdateTransaction: " + response.Content, response.Content);
      }
      return (UpdateTransactionOut) this.apiClient.Deserialize(response.Content, typeof(UpdateTransactionOut));
    }
    
    /// <summary>
    /// Delete transaction 
    /// </summary>
    /// <param name="Key">Transaction key</param>
    /// <returns>CancelTransactionOut</returns>
    public CancelTransactionOut CancelTransaction (string Key) {

      
      // verify the required parameter 'Key' is set
      if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling CancelTransaction");
      

      var path = "/api/v1/transactions/{key}";
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
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling CancelTransaction: " + response.Content, response.Content);
      }
      return (CancelTransactionOut) this.apiClient.Deserialize(response.Content, typeof(CancelTransactionOut));
    }
	
	 /// <summary>
    /// Delete transaction 
    /// </summary>
    /// <param name="Key">Transaction key</param>
    /// <returns>CancelTransactionOut</returns>
    public async Task<CancelTransactionOut> CancelTransactionAsync (string Key) {

      
          // verify the required parameter 'Key' is set
          if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling CancelTransaction");
      

      var path = "/api/v1/transactions/{key}";
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
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling CancelTransaction: " + response.Content, response.Content);
      }
      return (CancelTransactionOut) this.apiClient.Deserialize(response.Content, typeof(CancelTransactionOut));
    }
    
    /// <summary>
    /// Confirm transaction 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>ConfirmTransactionOut</returns>
    public ConfirmTransactionOut ConfirmTransaction (string Key, ConfirmTransactionIn Input) {

      
      // verify the required parameter 'Key' is set
      if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling ConfirmTransaction");
      
      // verify the required parameter 'Input' is set
      if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling ConfirmTransaction");
      

      var path = "/api/v1/transactions/{key}/confirm";
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
        throw new ApiException ((int)response.StatusCode, "Error calling ConfirmTransaction: " + response.Content, response.Content);
      }
      return (ConfirmTransactionOut) this.apiClient.Deserialize(response.Content, typeof(ConfirmTransactionOut));
    }
	
	 /// <summary>
    /// Confirm transaction 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>ConfirmTransactionOut</returns>
    public async Task<ConfirmTransactionOut> ConfirmTransactionAsync (string Key, ConfirmTransactionIn Input) {

      
          // verify the required parameter 'Key' is set
          if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling ConfirmTransaction");
      
          // verify the required parameter 'Input' is set
          if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling ConfirmTransaction");
      

      var path = "/api/v1/transactions/{key}/confirm";
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
        throw new ApiException ((int)response.StatusCode, "Error calling ConfirmTransaction: " + response.Content, response.Content);
      }
      return (ConfirmTransactionOut) this.apiClient.Deserialize(response.Content, typeof(ConfirmTransactionOut));
    }
    
    /// <summary>
    /// Un-confirm the transaction. Un-confirmed transaction can be edited or canceled like a newly created one. 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>UnconfirmTransactionOut</returns>
    public UnconfirmTransactionOut UnconfirmTransaction (string Key, UnconfirmTransactionIn Input) {

      
      // verify the required parameter 'Key' is set
      if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling UnconfirmTransaction");
      
      // verify the required parameter 'Input' is set
      if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling UnconfirmTransaction");
      

      var path = "/api/v1/transactions/{key}/unconfirm";
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
        throw new ApiException ((int)response.StatusCode, "Error calling UnconfirmTransaction: " + response.Content, response.Content);
      }
      return (UnconfirmTransactionOut) this.apiClient.Deserialize(response.Content, typeof(UnconfirmTransactionOut));
    }
	
	 /// <summary>
    /// Un-confirm the transaction. Un-confirmed transaction can be edited or canceled like a newly created one. 
    /// </summary>
    /// <param name="Key">Transaction key.</param>/// <param name="Input">Input</param>
    /// <returns>UnconfirmTransactionOut</returns>
    public async Task<UnconfirmTransactionOut> UnconfirmTransactionAsync (string Key, UnconfirmTransactionIn Input) {

      
          // verify the required parameter 'Key' is set
          if (Key == null) throw new ApiException(400, "Missing required parameter 'Key' when calling UnconfirmTransaction");
      
          // verify the required parameter 'Input' is set
          if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling UnconfirmTransaction");
      

      var path = "/api/v1/transactions/{key}/unconfirm";
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
        throw new ApiException ((int)response.StatusCode, "Error calling UnconfirmTransaction: " + response.Content, response.Content);
      }
      return (UnconfirmTransactionOut) this.apiClient.Deserialize(response.Content, typeof(UnconfirmTransactionOut));
    }
    
  }  
  
}
