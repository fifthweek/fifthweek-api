namespace Taxamo.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestSharp;

    using Taxamo.Client;
    using Taxamo.Model;

    public interface IApivtaxApi {
    
    /// <summary>
    /// Simple tax 
    /// </summary>
    /// <param name="ProductType">Product type, according to dictionary /dictionaries/product_types. </param>/// <param name="BuyerCreditCardPrefix">Buyer&#39;s credit card prefix.</param>/// <param name="CurrencyCode">Currency code for transaction - e.g. EUR.</param>/// <param name="UnitPrice">Unit price.</param>/// <param name="Quantity">Quantity Defaults to 1.</param>/// <param name="BuyerTaxNumber"> Buyer&#39;s tax number - EU VAT number for example. If using EU VAT number, it is possible to provide country code in it (e.g. IE1234567X) or simply use billing_country_code field for that. In the first case, if billing_country_code value was provided, it will be overwritten with country code value extracted from VAT number - but only if the VAT has been verified properly.</param>/// <param name="ForceCountryCode">Two-letter ISO country code, e.g. FR. Use it to force country code for tax calculation.</param>/// <param name="OrderDate">Order date in yyyy-MM-dd format, in merchant&#39;s timezone. If provided by the API caller, no timezone conversion is performed. Default value is current date and time. When using public token, the default value is used.</param>/// <param name="Amount">Amount. Required if total amount is not provided.</param>/// <param name="BillingCountryCode">Billing two letter ISO country code.</param>/// <param name="TotalAmount">Total amount. Required if amount is not provided.</param>/// <param name="TaxDeducted">If the transaction is in a country supported by Taxamo, but the tax is not calculated due to merchant settings or EU B2B transaction for example.</param>
    /// <returns>CalculateSimpleTaxOut</returns>
    CalculateSimpleTaxOut CalculateSimpleTax (string ProductType, string BuyerCreditCardPrefix, string CurrencyCode, decimal? UnitPrice, decimal? Quantity, string BuyerTaxNumber, string ForceCountryCode, string OrderDate, decimal? Amount, string BillingCountryCode, decimal? TotalAmount, bool? TaxDeducted);

    /// <summary>
    /// Simple tax 
    /// </summary>
    /// <param name="ProductType">Product type, according to dictionary /dictionaries/product_types. </param>/// <param name="BuyerCreditCardPrefix">Buyer&#39;s credit card prefix.</param>/// <param name="CurrencyCode">Currency code for transaction - e.g. EUR.</param>/// <param name="UnitPrice">Unit price.</param>/// <param name="Quantity">Quantity Defaults to 1.</param>/// <param name="BuyerTaxNumber"> Buyer&#39;s tax number - EU VAT number for example. If using EU VAT number, it is possible to provide country code in it (e.g. IE1234567X) or simply use billing_country_code field for that. In the first case, if billing_country_code value was provided, it will be overwritten with country code value extracted from VAT number - but only if the VAT has been verified properly.</param>/// <param name="ForceCountryCode">Two-letter ISO country code, e.g. FR. Use it to force country code for tax calculation.</param>/// <param name="OrderDate">Order date in yyyy-MM-dd format, in merchant&#39;s timezone. If provided by the API caller, no timezone conversion is performed. Default value is current date and time. When using public token, the default value is used.</param>/// <param name="Amount">Amount. Required if total amount is not provided.</param>/// <param name="BillingCountryCode">Billing two letter ISO country code.</param>/// <param name="TotalAmount">Total amount. Required if amount is not provided.</param>/// <param name="TaxDeducted">If the transaction is in a country supported by Taxamo, but the tax is not calculated due to merchant settings or EU B2B transaction for example.</param>
    /// <returns>CalculateSimpleTaxOut</returns>
    Task<CalculateSimpleTaxOut> CalculateSimpleTaxAsync (string ProductType, string BuyerCreditCardPrefix, string CurrencyCode, decimal? UnitPrice, decimal? Quantity, string BuyerTaxNumber, string ForceCountryCode, string OrderDate, decimal? Amount, string BillingCountryCode, decimal? TotalAmount, bool? TaxDeducted);
    
    /// <summary>
    /// Calculate tax 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CalculateTaxOut</returns>
    CalculateTaxOut CalculateTax (CalculateTaxIn Input);

    /// <summary>
    /// Calculate tax 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CalculateTaxOut</returns>
    Task<CalculateTaxOut> CalculateTaxAsync (CalculateTaxIn Input);
    
    /// <summary>
    /// Calculate location 
    /// </summary>
    /// <param name="BillingCountryCode">Billing two letter ISO country code.</param>/// <param name="BuyerCreditCardPrefix">Buyer&#39;s credit card prefix.</param>
    /// <returns>CalculateTaxLocationOut</returns>
    CalculateTaxLocationOut CalculateTaxLocation (string BillingCountryCode, string BuyerCreditCardPrefix);

    /// <summary>
    /// Calculate location 
    /// </summary>
    /// <param name="BillingCountryCode">Billing two letter ISO country code.</param>/// <param name="BuyerCreditCardPrefix">Buyer&#39;s credit card prefix.</param>
    /// <returns>CalculateTaxLocationOut</returns>
    Task<CalculateTaxLocationOut> CalculateTaxLocationAsync (string BillingCountryCode, string BuyerCreditCardPrefix);
    
    /// <summary>
    /// Validate VAT number 
    /// </summary>
    /// <param name="CountryCode">Two-letter ISO country code.</param>/// <param name="TaxNumber">Tax number</param>
    /// <returns>ValidateTaxNumberOut</returns>
    ValidateTaxNumberOut ValidateTaxNumber (string CountryCode, string TaxNumber);

    /// <summary>
    /// Validate VAT number 
    /// </summary>
    /// <param name="CountryCode">Two-letter ISO country code.</param>/// <param name="TaxNumber">Tax number</param>
    /// <returns>ValidateTaxNumberOut</returns>
    Task<ValidateTaxNumberOut> ValidateTaxNumberAsync (string CountryCode, string TaxNumber);
    
  }

  /// <summary>
  /// Represents a collection of functions to interact with the API endpoints
  /// </summary>
  public class ApivtaxApi : IApivtaxApi {

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivtaxApi"/> class.
    /// </summary>
    /// <param name="apiClient"> an instance of ApiClient (optional)
    /// <returns></returns>
    public ApivtaxApi(ApiClient apiClient = null) {
      if (apiClient == null) { // use the default one in Configuration
        this.apiClient = Configuration.apiClient; 
      } else {
        this.apiClient = apiClient;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApivtaxApi"/> class.
    /// </summary>
    /// <returns></returns>
    public ApivtaxApi(String basePath)
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
    /// Simple tax 
    /// </summary>
    /// <param name="ProductType">Product type, according to dictionary /dictionaries/product_types. </param>/// <param name="BuyerCreditCardPrefix">Buyer&#39;s credit card prefix.</param>/// <param name="CurrencyCode">Currency code for transaction - e.g. EUR.</param>/// <param name="UnitPrice">Unit price.</param>/// <param name="Quantity">Quantity Defaults to 1.</param>/// <param name="BuyerTaxNumber"> Buyer&#39;s tax number - EU VAT number for example. If using EU VAT number, it is possible to provide country code in it (e.g. IE1234567X) or simply use billing_country_code field for that. In the first case, if billing_country_code value was provided, it will be overwritten with country code value extracted from VAT number - but only if the VAT has been verified properly.</param>/// <param name="ForceCountryCode">Two-letter ISO country code, e.g. FR. Use it to force country code for tax calculation.</param>/// <param name="OrderDate">Order date in yyyy-MM-dd format, in merchant&#39;s timezone. If provided by the API caller, no timezone conversion is performed. Default value is current date and time. When using public token, the default value is used.</param>/// <param name="Amount">Amount. Required if total amount is not provided.</param>/// <param name="BillingCountryCode">Billing two letter ISO country code.</param>/// <param name="TotalAmount">Total amount. Required if amount is not provided.</param>/// <param name="TaxDeducted">If the transaction is in a country supported by Taxamo, but the tax is not calculated due to merchant settings or EU B2B transaction for example.</param>
    /// <returns>CalculateSimpleTaxOut</returns>
    public CalculateSimpleTaxOut CalculateSimpleTax (string ProductType, string BuyerCreditCardPrefix, string CurrencyCode, decimal? UnitPrice, decimal? Quantity, string BuyerTaxNumber, string ForceCountryCode, string OrderDate, decimal? Amount, string BillingCountryCode, decimal? TotalAmount, bool? TaxDeducted) {

      
      // verify the required parameter 'CurrencyCode' is set
      if (CurrencyCode == null) throw new ApiException(400, "Missing required parameter 'CurrencyCode' when calling CalculateSimpleTax");
      

      var path = "/api/v1/tax/calculate";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (ProductType != null) queryParams.Add("product_type", this.apiClient.ParameterToString(ProductType)); // query parameter
       if (BuyerCreditCardPrefix != null) queryParams.Add("buyer_credit_card_prefix", this.apiClient.ParameterToString(BuyerCreditCardPrefix)); // query parameter
       if (CurrencyCode != null) queryParams.Add("currency_code", this.apiClient.ParameterToString(CurrencyCode)); // query parameter
       if (UnitPrice != null) queryParams.Add("unit_price", this.apiClient.ParameterToString(UnitPrice)); // query parameter
       if (Quantity != null) queryParams.Add("quantity", this.apiClient.ParameterToString(Quantity)); // query parameter
       if (BuyerTaxNumber != null) queryParams.Add("buyer_tax_number", this.apiClient.ParameterToString(BuyerTaxNumber)); // query parameter
       if (ForceCountryCode != null) queryParams.Add("force_country_code", this.apiClient.ParameterToString(ForceCountryCode)); // query parameter
       if (OrderDate != null) queryParams.Add("order_date", this.apiClient.ParameterToString(OrderDate)); // query parameter
       if (Amount != null) queryParams.Add("amount", this.apiClient.ParameterToString(Amount)); // query parameter
       if (BillingCountryCode != null) queryParams.Add("billing_country_code", this.apiClient.ParameterToString(BillingCountryCode)); // query parameter
       if (TotalAmount != null) queryParams.Add("total_amount", this.apiClient.ParameterToString(TotalAmount)); // query parameter
       if (TaxDeducted != null) queryParams.Add("tax_deducted", this.apiClient.ParameterToString(TaxDeducted)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling CalculateSimpleTax: " + response.Content, response.Content);
      }
      return (CalculateSimpleTaxOut) this.apiClient.Deserialize(response.Content, typeof(CalculateSimpleTaxOut));
    }
	
	 /// <summary>
    /// Simple tax 
    /// </summary>
    /// <param name="ProductType">Product type, according to dictionary /dictionaries/product_types. </param>/// <param name="BuyerCreditCardPrefix">Buyer&#39;s credit card prefix.</param>/// <param name="CurrencyCode">Currency code for transaction - e.g. EUR.</param>/// <param name="UnitPrice">Unit price.</param>/// <param name="Quantity">Quantity Defaults to 1.</param>/// <param name="BuyerTaxNumber"> Buyer&#39;s tax number - EU VAT number for example. If using EU VAT number, it is possible to provide country code in it (e.g. IE1234567X) or simply use billing_country_code field for that. In the first case, if billing_country_code value was provided, it will be overwritten with country code value extracted from VAT number - but only if the VAT has been verified properly.</param>/// <param name="ForceCountryCode">Two-letter ISO country code, e.g. FR. Use it to force country code for tax calculation.</param>/// <param name="OrderDate">Order date in yyyy-MM-dd format, in merchant&#39;s timezone. If provided by the API caller, no timezone conversion is performed. Default value is current date and time. When using public token, the default value is used.</param>/// <param name="Amount">Amount. Required if total amount is not provided.</param>/// <param name="BillingCountryCode">Billing two letter ISO country code.</param>/// <param name="TotalAmount">Total amount. Required if amount is not provided.</param>/// <param name="TaxDeducted">If the transaction is in a country supported by Taxamo, but the tax is not calculated due to merchant settings or EU B2B transaction for example.</param>
    /// <returns>CalculateSimpleTaxOut</returns>
    public async Task<CalculateSimpleTaxOut> CalculateSimpleTaxAsync (string ProductType, string BuyerCreditCardPrefix, string CurrencyCode, decimal? UnitPrice, decimal? Quantity, string BuyerTaxNumber, string ForceCountryCode, string OrderDate, decimal? Amount, string BillingCountryCode, decimal? TotalAmount, bool? TaxDeducted) {

      
          // verify the required parameter 'CurrencyCode' is set
          if (CurrencyCode == null) throw new ApiException(400, "Missing required parameter 'CurrencyCode' when calling CalculateSimpleTax");
      

      var path = "/api/v1/tax/calculate";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (ProductType != null) queryParams.Add("product_type", this.apiClient.ParameterToString(ProductType)); // query parameter
       if (BuyerCreditCardPrefix != null) queryParams.Add("buyer_credit_card_prefix", this.apiClient.ParameterToString(BuyerCreditCardPrefix)); // query parameter
       if (CurrencyCode != null) queryParams.Add("currency_code", this.apiClient.ParameterToString(CurrencyCode)); // query parameter
       if (UnitPrice != null) queryParams.Add("unit_price", this.apiClient.ParameterToString(UnitPrice)); // query parameter
       if (Quantity != null) queryParams.Add("quantity", this.apiClient.ParameterToString(Quantity)); // query parameter
       if (BuyerTaxNumber != null) queryParams.Add("buyer_tax_number", this.apiClient.ParameterToString(BuyerTaxNumber)); // query parameter
       if (ForceCountryCode != null) queryParams.Add("force_country_code", this.apiClient.ParameterToString(ForceCountryCode)); // query parameter
       if (OrderDate != null) queryParams.Add("order_date", this.apiClient.ParameterToString(OrderDate)); // query parameter
       if (Amount != null) queryParams.Add("amount", this.apiClient.ParameterToString(Amount)); // query parameter
       if (BillingCountryCode != null) queryParams.Add("billing_country_code", this.apiClient.ParameterToString(BillingCountryCode)); // query parameter
       if (TotalAmount != null) queryParams.Add("total_amount", this.apiClient.ParameterToString(TotalAmount)); // query parameter
       if (TaxDeducted != null) queryParams.Add("tax_deducted", this.apiClient.ParameterToString(TaxDeducted)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling CalculateSimpleTax: " + response.Content, response.Content);
      }
      return (CalculateSimpleTaxOut) this.apiClient.Deserialize(response.Content, typeof(CalculateSimpleTaxOut));
    }
    
    /// <summary>
    /// Calculate tax 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CalculateTaxOut</returns>
    public CalculateTaxOut CalculateTax (CalculateTaxIn Input) {

      
      // verify the required parameter 'Input' is set
      if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling CalculateTax");
      

      var path = "/api/v1/tax/calculate";
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
        throw new ApiException ((int)response.StatusCode, "Error calling CalculateTax: " + response.Content, response.Content);
      }
      return (CalculateTaxOut) this.apiClient.Deserialize(response.Content, typeof(CalculateTaxOut));
    }
	
	 /// <summary>
    /// Calculate tax 
    /// </summary>
    /// <param name="Input">Input</param>
    /// <returns>CalculateTaxOut</returns>
    public async Task<CalculateTaxOut> CalculateTaxAsync (CalculateTaxIn Input) {

      
          // verify the required parameter 'Input' is set
          if (Input == null) throw new ApiException(400, "Missing required parameter 'Input' when calling CalculateTax");
      

      var path = "/api/v1/tax/calculate";
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
	     if (((int)response.StatusCode) == 400)
	     {
             // This is to get around an API change where insufficient evidence now returns a 
             // 400 error code but all the data we need is still there.
             var transaction = (Transaction)this.apiClient.Deserialize(response.Content, typeof(Transaction));
	         return new CalculateTaxOut { Transaction = transaction };
	     }
	   
        if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling CalculateTax: " + response.Content, response.Content);
      }
      return (CalculateTaxOut) this.apiClient.Deserialize(response.Content, typeof(CalculateTaxOut));
    }
    
    /// <summary>
    /// Calculate location 
    /// </summary>
    /// <param name="BillingCountryCode">Billing two letter ISO country code.</param>/// <param name="BuyerCreditCardPrefix">Buyer&#39;s credit card prefix.</param>
    /// <returns>CalculateTaxLocationOut</returns>
    public CalculateTaxLocationOut CalculateTaxLocation (string BillingCountryCode, string BuyerCreditCardPrefix) {

      

      var path = "/api/v1/tax/location/calculate";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (BillingCountryCode != null) queryParams.Add("billing_country_code", this.apiClient.ParameterToString(BillingCountryCode)); // query parameter
       if (BuyerCreditCardPrefix != null) queryParams.Add("buyer_credit_card_prefix", this.apiClient.ParameterToString(BuyerCreditCardPrefix)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling CalculateTaxLocation: " + response.Content, response.Content);
      }
      return (CalculateTaxLocationOut) this.apiClient.Deserialize(response.Content, typeof(CalculateTaxLocationOut));
    }
	
	 /// <summary>
    /// Calculate location 
    /// </summary>
    /// <param name="BillingCountryCode">Billing two letter ISO country code.</param>/// <param name="BuyerCreditCardPrefix">Buyer&#39;s credit card prefix.</param>
    /// <returns>CalculateTaxLocationOut</returns>
    public async Task<CalculateTaxLocationOut> CalculateTaxLocationAsync (string BillingCountryCode, string BuyerCreditCardPrefix) {

      

      var path = "/api/v1/tax/location/calculate";
      path = path.Replace("{format}", "json");
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (BillingCountryCode != null) queryParams.Add("billing_country_code", this.apiClient.ParameterToString(BillingCountryCode)); // query parameter
       if (BuyerCreditCardPrefix != null) queryParams.Add("buyer_credit_card_prefix", this.apiClient.ParameterToString(BuyerCreditCardPrefix)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling CalculateTaxLocation: " + response.Content, response.Content);
      }
      return (CalculateTaxLocationOut) this.apiClient.Deserialize(response.Content, typeof(CalculateTaxLocationOut));
    }
    
    /// <summary>
    /// Validate VAT number 
    /// </summary>
    /// <param name="CountryCode">Two-letter ISO country code.</param>/// <param name="TaxNumber">Tax number</param>
    /// <returns>ValidateTaxNumberOut</returns>
    public ValidateTaxNumberOut ValidateTaxNumber (string CountryCode, string TaxNumber) {

      
      // verify the required parameter 'TaxNumber' is set
      if (TaxNumber == null) throw new ApiException(400, "Missing required parameter 'TaxNumber' when calling ValidateTaxNumber");
      

      var path = "/api/v1/tax/vat_numbers/{tax_number}/validate";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "tax_number" + "}", this.apiClient.ParameterToString(TaxNumber));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (CountryCode != null) queryParams.Add("country_code", this.apiClient.ParameterToString(CountryCode)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) this.apiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling ValidateTaxNumber: " + response.Content, response.Content);
      }
      return (ValidateTaxNumberOut) this.apiClient.Deserialize(response.Content, typeof(ValidateTaxNumberOut));
    }
	
	 /// <summary>
    /// Validate VAT number 
    /// </summary>
    /// <param name="CountryCode">Two-letter ISO country code.</param>/// <param name="TaxNumber">Tax number</param>
    /// <returns>ValidateTaxNumberOut</returns>
    public async Task<ValidateTaxNumberOut> ValidateTaxNumberAsync (string CountryCode, string TaxNumber) {

      
          // verify the required parameter 'TaxNumber' is set
          if (TaxNumber == null) throw new ApiException(400, "Missing required parameter 'TaxNumber' when calling ValidateTaxNumber");
      

      var path = "/api/v1/tax/vat_numbers/{tax_number}/validate";
      path = path.Replace("{format}", "json");
      path = path.Replace("{" + "tax_number" + "}", this.apiClient.ParameterToString(TaxNumber));
      

      var queryParams = new Dictionary<String, String>();
      var headerParams = new Dictionary<String, String>();
      var formParams = new Dictionary<String, String>();
      var fileParams = new Dictionary<String, String>();
      String postBody = null;

       if (CountryCode != null) queryParams.Add("country_code", this.apiClient.ParameterToString(CountryCode)); // query parameter
      
      
      
      

      // authentication setting, if any
      String[] authSettings = new String[] {  };

      // make the HTTP request
      IRestResponse response = (IRestResponse) await this.apiClient.CallApiAsync(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (((int)response.StatusCode) >= 400) {
        throw new ApiException ((int)response.StatusCode, "Error calling ValidateTaxNumber: " + response.Content, response.Content);
      }
      return (ValidateTaxNumberOut) this.apiClient.Deserialize(response.Content, typeof(ValidateTaxNumberOut));
    }
    
  }  
  
}
