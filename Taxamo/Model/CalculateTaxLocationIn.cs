namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CalculateTaxLocationIn {
    
    /* Billing two letter ISO country code. */
    [DataMember(Name="billing_country_code", EmitDefaultValue=false)]
    public string BillingCountryCode { get; set; }

    
    /* Buyer's credit card prefix. */
    [DataMember(Name="buyer_credit_card_prefix", EmitDefaultValue=false)]
    public string BuyerCreditCardPrefix { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CalculateTaxLocationIn {\n");
      
      sb.Append("  BillingCountryCode: ").Append(this.BillingCountryCode).Append("\n");
      
      sb.Append("  BuyerCreditCardPrefix: ").Append(this.BuyerCreditCardPrefix).Append("\n");
      
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