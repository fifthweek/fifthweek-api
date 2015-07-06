namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CalculateSimpleTaxIn {
    
    /* Product type, according to dictionary /dictionaries/product_types.  */
    [DataMember(Name="product_type", EmitDefaultValue=false)]
    public string ProductType { get; set; }

    
    /* Buyer's credit card prefix. */
    [DataMember(Name="buyer_credit_card_prefix", EmitDefaultValue=false)]
    public string BuyerCreditCardPrefix { get; set; }

    
    /* Currency code for transaction - e.g. EUR. */
    [DataMember(Name="currency_code", EmitDefaultValue=false)]
    public string CurrencyCode { get; set; }

    
    /* Unit price. */
    [DataMember(Name="unit_price", EmitDefaultValue=false)]
    public double? UnitPrice { get; set; }

    
    /* Quantity Defaults to 1. */
    [DataMember(Name="quantity", EmitDefaultValue=false)]
    public double? Quantity { get; set; }

    
    /*  Buyer's tax number - EU VAT number for example. If using EU VAT number, it is possible to provide country code in it (e.g. IE1234567X) or simply use billing_country_code field for that. In the first case, if billing_country_code value was provided, it will be overwritten with country code value extracted from VAT number - but only if the VAT has been verified properly. */
    [DataMember(Name="buyer_tax_number", EmitDefaultValue=false)]
    public string BuyerTaxNumber { get; set; }

    
    /* Two-letter ISO country code, e.g. FR. Use it to force country code for tax calculation. */
    [DataMember(Name="force_country_code", EmitDefaultValue=false)]
    public string ForceCountryCode { get; set; }

    
    /* Order date in yyyy-MM-dd format, in merchant's timezone. If provided by the API caller, no timezone conversion is performed. Default value is current date and time. When using public token, the default value is used. */
    [DataMember(Name="order_date", EmitDefaultValue=false)]
    public string OrderDate { get; set; }

    
    /* Amount. Required if total amount is not provided. */
    [DataMember(Name="amount", EmitDefaultValue=false)]
    public double? Amount { get; set; }

    
    /* Billing two letter ISO country code. */
    [DataMember(Name="billing_country_code", EmitDefaultValue=false)]
    public string BillingCountryCode { get; set; }

    
    /* Total amount. Required if amount is not provided. */
    [DataMember(Name="total_amount", EmitDefaultValue=false)]
    public double? TotalAmount { get; set; }

    
    /* If the transaction is in a country supported by Taxamo, but the tax is not calculated due to merchant settings or EU B2B transaction for example. */
    [DataMember(Name="tax_deducted", EmitDefaultValue=false)]
    public bool? TaxDeducted { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CalculateSimpleTaxIn {\n");
      
      sb.Append("  ProductType: ").Append(this.ProductType).Append("\n");
      
      sb.Append("  BuyerCreditCardPrefix: ").Append(this.BuyerCreditCardPrefix).Append("\n");
      
      sb.Append("  CurrencyCode: ").Append(this.CurrencyCode).Append("\n");
      
      sb.Append("  UnitPrice: ").Append(this.UnitPrice).Append("\n");
      
      sb.Append("  Quantity: ").Append(this.Quantity).Append("\n");
      
      sb.Append("  BuyerTaxNumber: ").Append(this.BuyerTaxNumber).Append("\n");
      
      sb.Append("  ForceCountryCode: ").Append(this.ForceCountryCode).Append("\n");
      
      sb.Append("  OrderDate: ").Append(this.OrderDate).Append("\n");
      
      sb.Append("  Amount: ").Append(this.Amount).Append("\n");
      
      sb.Append("  BillingCountryCode: ").Append(this.BillingCountryCode).Append("\n");
      
      sb.Append("  TotalAmount: ").Append(this.TotalAmount).Append("\n");
      
      sb.Append("  TaxDeducted: ").Append(this.TaxDeducted).Append("\n");
      
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