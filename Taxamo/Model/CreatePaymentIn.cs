namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CreatePaymentIn {
    
    /* Amount that has been paid. Use negative value to register refunds. */
    [DataMember(Name="amount", EmitDefaultValue=false)]
    public decimal? Amount { get; set; }

    
    /* When the payment was received in yyyy-MM-dd'T'HH:mm:ss(.SSS)'Z' format (24 hour, UTC timezone). Defaults to current date and time. */
    [DataMember(Name="payment_timestamp", EmitDefaultValue=false)]
    public string PaymentTimestamp { get; set; }

    
    /* Additional payment information. */
    [DataMember(Name="payment_information", EmitDefaultValue=false)]
    public string PaymentInformation { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CreatePaymentIn {\n");
      
      sb.Append("  Amount: ").Append(this.Amount).Append("\n");
      
      sb.Append("  PaymentTimestamp: ").Append(this.PaymentTimestamp).Append("\n");
      
      sb.Append("  PaymentInformation: ").Append(this.PaymentInformation).Append("\n");
      
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