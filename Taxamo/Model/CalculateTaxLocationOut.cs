namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CalculateTaxLocationOut {
    
    /* Two-letter ISO country code, e.g. FR. This code applies to detected/set country for transaction, but can be set using manual mode. */
    [DataMember(Name="tax_country_code", EmitDefaultValue=false)]
    public string TaxCountryCode { get; set; }

    
    /* If the transaction is in a country supported by Taxamo, but the tax is not calculated due to merchant settings or EU B2B transaction for example. */
    [DataMember(Name="tax_deducted", EmitDefaultValue=false)]
    public bool? TaxDeducted { get; set; }

    
    /* Is tax calculation supported for a detected tax location? */
    [DataMember(Name="tax_supported", EmitDefaultValue=false)]
    public bool? TaxSupported { get; set; }

    
    /* Map of countries calculated from evidence provided. This value is not stored and is available only upon tax calculation. */
    [DataMember(Name="countries", EmitDefaultValue=false)]
    public Countries Countries { get; set; }

    
    /* IP address of the buyer in dotted decimal (IPv4) or text format (IPv6). */
    [DataMember(Name="buyer_ip", EmitDefaultValue=false)]
    public string BuyerIp { get; set; }

    
    /* Billing two letter ISO country code. */
    [DataMember(Name="billing_country_code", EmitDefaultValue=false)]
    public string BillingCountryCode { get; set; }

    
    /* Buyer's credit card prefix. */
    [DataMember(Name="buyer_credit_card_prefix", EmitDefaultValue=false)]
    public string BuyerCreditCardPrefix { get; set; }

    
    /* Tax country of residence evidence. */
    [DataMember(Name="evidence", EmitDefaultValue=false)]
    public Evidence Evidence { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CalculateTaxLocationOut {\n");
      
      sb.Append("  TaxCountryCode: ").Append(this.TaxCountryCode).Append("\n");
      
      sb.Append("  TaxDeducted: ").Append(this.TaxDeducted).Append("\n");
      
      sb.Append("  TaxSupported: ").Append(this.TaxSupported).Append("\n");
      
      sb.Append("  Countries: ").Append(this.Countries).Append("\n");
      
      sb.Append("  BuyerIp: ").Append(this.BuyerIp).Append("\n");
      
      sb.Append("  BillingCountryCode: ").Append(this.BillingCountryCode).Append("\n");
      
      sb.Append("  BuyerCreditCardPrefix: ").Append(this.BuyerCreditCardPrefix).Append("\n");
      
      sb.Append("  Evidence: ").Append(this.Evidence).Append("\n");
      
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