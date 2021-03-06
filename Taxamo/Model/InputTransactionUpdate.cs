namespace Taxamo.Model {
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class InputTransactionUpdate {
    
    /* Buyer's credit card prefix. */
    [DataMember(Name="buyer_credit_card_prefix", EmitDefaultValue=false)]
    public string BuyerCreditCardPrefix { get; set; }

    
    /* Custom data related to transaction. */
    [DataMember(Name="custom_data", EmitDefaultValue=false)]
    public string CustomData { get; set; }

    
    /* Buyer's name - first name and last name or company name. */
    [DataMember(Name="buyer_name", EmitDefaultValue=false)]
    public string BuyerName { get; set; }

    
    /* Invoice date of issue. */
    [DataMember(Name="invoice_date", EmitDefaultValue=false)]
    public string InvoiceDate { get; set; }

    
    /* Currency code for transaction - e.g. EUR. */
    [DataMember(Name="currency_code", EmitDefaultValue=false)]
    public string CurrencyCode { get; set; }

    
    /* Supply date in yyyy-MM-dd format. */
    [DataMember(Name="supply_date", EmitDefaultValue=false)]
    public string SupplyDate { get; set; }

    
    /* Invoice address. */
    [DataMember(Name="invoice_address", EmitDefaultValue=false)]
    public InvoiceAddress InvoiceAddress { get; set; }

    
    /* Verification token */
    [DataMember(Name="verification_token", EmitDefaultValue=false)]
    public string VerificationToken { get; set; }

    
    /* Transaction lines. */
    [DataMember(Name="transaction_lines", EmitDefaultValue=false)]
    public List<InputTransactionLine> TransactionLines { get; set; }

    
    /*  Buyer's tax number - EU VAT number for example. If using EU VAT number, it is possible to provide country code in it (e.g. IE1234567X) or simply use billing_country_code field for that. In the first case, if billing_country_code value was provided, it will be overwritten with country code value extracted from VAT number - but only if the VAT has been verified properly. */
    [DataMember(Name="buyer_tax_number", EmitDefaultValue=false)]
    public string BuyerTaxNumber { get; set; }

    
    /* Custom fields, stored as key-value pairs. This property is not processed and used mostly with Taxamo-built helpers. */
    [DataMember(Name="custom_fields", EmitDefaultValue=false)]
    public List<CustomFields> CustomFields { get; set; }

    
    /* Two-letter ISO country code, e.g. FR. Use it to force country code for tax calculation. */
    [DataMember(Name="force_country_code", EmitDefaultValue=false)]
    public string ForceCountryCode { get; set; }

    
    /* Invoice number. */
    [DataMember(Name="invoice_number", EmitDefaultValue=false)]
    public string InvoiceNumber { get; set; }

    
    /* Order date in yyyy-MM-dd format, in merchant's timezone. If provided by the API caller, no timezone conversion is performed. Default value is current date and time. When using public token, the default value is used. */
    [DataMember(Name="order_date", EmitDefaultValue=false)]
    public string OrderDate { get; set; }

    
    /* IP address of the buyer in dotted decimal (IPv4) or text format (IPv6). */
    [DataMember(Name="buyer_ip", EmitDefaultValue=false)]
    public string BuyerIp { get; set; }

    
    /* Buyer's declared email address. */
    [DataMember(Name="buyer_email", EmitDefaultValue=false)]
    public string BuyerEmail { get; set; }

    
    /* Use data and evidence from original transaction. Tax will be re-calculated, but evidence won't be re-checked. This parameter is taken into account only when 'manual' flag is raised. */
    [DataMember(Name="original_transaction_key", EmitDefaultValue=false)]
    public string OriginalTransactionKey { get; set; }

    
    /* Billing two letter ISO country code. */
    [DataMember(Name="billing_country_code", EmitDefaultValue=false)]
    public string BillingCountryCode { get; set; }

    
    /* Custom identifier provided upon transaction creation. */
    [DataMember(Name="custom_id", EmitDefaultValue=false)]
    public string CustomId { get; set; }

    
    /* Additional currency information - can be used to receive additional information about invoice in another currency. */
    [DataMember(Name="additional_currencies", EmitDefaultValue=false)]
    public AdditionalCurrencies AdditionalCurrencies { get; set; }

    
    /* Invoice place of issue. */
    [DataMember(Name="invoice_place", EmitDefaultValue=false)]
    public string InvoicePlace { get; set; }

    
    /* Tax country of residence evidence. */
    [DataMember(Name="evidence", EmitDefaultValue=false)]
    public Evidence Evidence { get; set; }

    
    /* Transaction description. */
    [DataMember(Name="description", EmitDefaultValue=false)]
    public string Description { get; set; }

    
    /* True if the transaction deducted from tax and no tax is applied. Either set automatically when VAT number validates with VIES correctly, but can also be provided in manual mode. */
    [DataMember(Name="tax_deducted", EmitDefaultValue=false)]
    public bool? TaxDeducted { get; set; }

    
    /* Two-letter ISO country code, e.g. FR. This code applies to detected/set country for transaction, but can be set using manual mode. */
    [DataMember(Name="tax_country_code", EmitDefaultValue=false)]
    public string TaxCountryCode { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class InputTransactionUpdate {\n");
      
      sb.Append("  BuyerCreditCardPrefix: ").Append(this.BuyerCreditCardPrefix).Append("\n");
      
      sb.Append("  CustomData: ").Append(this.CustomData).Append("\n");
      
      sb.Append("  BuyerName: ").Append(this.BuyerName).Append("\n");
      
      sb.Append("  InvoiceDate: ").Append(this.InvoiceDate).Append("\n");
      
      sb.Append("  CurrencyCode: ").Append(this.CurrencyCode).Append("\n");
      
      sb.Append("  SupplyDate: ").Append(this.SupplyDate).Append("\n");
      
      sb.Append("  InvoiceAddress: ").Append(this.InvoiceAddress).Append("\n");
      
      sb.Append("  VerificationToken: ").Append(this.VerificationToken).Append("\n");
      
      sb.Append("  TransactionLines: ").Append(this.TransactionLines).Append("\n");
      
      sb.Append("  BuyerTaxNumber: ").Append(this.BuyerTaxNumber).Append("\n");
      
      sb.Append("  CustomFields: ").Append(this.CustomFields).Append("\n");
      
      sb.Append("  ForceCountryCode: ").Append(this.ForceCountryCode).Append("\n");
      
      sb.Append("  InvoiceNumber: ").Append(this.InvoiceNumber).Append("\n");
      
      sb.Append("  OrderDate: ").Append(this.OrderDate).Append("\n");
      
      sb.Append("  BuyerIp: ").Append(this.BuyerIp).Append("\n");
      
      sb.Append("  BuyerEmail: ").Append(this.BuyerEmail).Append("\n");
      
      sb.Append("  OriginalTransactionKey: ").Append(this.OriginalTransactionKey).Append("\n");
      
      sb.Append("  BillingCountryCode: ").Append(this.BillingCountryCode).Append("\n");
      
      sb.Append("  CustomId: ").Append(this.CustomId).Append("\n");
      
      sb.Append("  AdditionalCurrencies: ").Append(this.AdditionalCurrencies).Append("\n");
      
      sb.Append("  InvoicePlace: ").Append(this.InvoicePlace).Append("\n");
      
      sb.Append("  Evidence: ").Append(this.Evidence).Append("\n");
      
      sb.Append("  Description: ").Append(this.Description).Append("\n");
      
      sb.Append("  TaxDeducted: ").Append(this.TaxDeducted).Append("\n");
      
      sb.Append("  TaxCountryCode: ").Append(this.TaxCountryCode).Append("\n");
      
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
  
  
}