namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ValidateTaxNumberOut {
    
    /* If the transaction is in a country supported by Taxamo, but the tax is not calculated due to merchant settings or EU B2B transaction for example. */
    [DataMember(Name="tax_deducted", EmitDefaultValue=false)]
    public bool? TaxDeducted { get; set; }

    
    /*  Buyer's tax number - EU VAT number for example. If using EU VAT number, it is possible to provide country code in it (e.g. IE1234567X) or simply use billing_country_code field for that. In the first case, if billing_country_code value was provided, it will be overwritten with country code value extracted from VAT number - but only if the VAT has been verified properly. */
    [DataMember(Name="buyer_tax_number", EmitDefaultValue=false)]
    public string BuyerTaxNumber { get; set; }

    
    /* If the buyer tax number has been provided and was validated successfully. Always true for domestic transactions (billing country same as merchant's country), tax number doesn't get validated in that case. */
    [DataMember(Name="buyer_tax_number_valid", EmitDefaultValue=false)]
    public bool? BuyerTaxNumberValid { get; set; }

    
    /* Billing two letter ISO country code. */
    [DataMember(Name="billing_country_code", EmitDefaultValue=false)]
    public string BillingCountryCode { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ValidateTaxNumberOut {\n");
      
      sb.Append("  TaxDeducted: ").Append(this.TaxDeducted).Append("\n");
      
      sb.Append("  BuyerTaxNumber: ").Append(this.BuyerTaxNumber).Append("\n");
      
      sb.Append("  BuyerTaxNumberValid: ").Append(this.BuyerTaxNumberValid).Append("\n");
      
      sb.Append("  BillingCountryCode: ").Append(this.BillingCountryCode).Append("\n");
      
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