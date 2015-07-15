namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CreateRefundIn {
    
    /* Line identifier. Either line key or custom id is required. */
    [DataMember(Name="line_key", EmitDefaultValue=false)]
    public string LineKey { get; set; }

    
    /* Line custom identifier. Either line key or custom id is required. */
    [DataMember(Name="custom_id", EmitDefaultValue=false)]
    public string CustomId { get; set; }

    
    /* Amount (without tax) to be refunded. Either amount or total amount is required. */
    [DataMember(Name="amount", EmitDefaultValue=false)]
    public decimal? Amount { get; set; }

    
    /* Total amount, including tax, to be refunded. Either amount or total amount is required. */
    [DataMember(Name="total_amount", EmitDefaultValue=false)]
    public decimal? TotalAmount { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CreateRefundIn {\n");
      
      sb.Append("  LineKey: ").Append(this.LineKey).Append("\n");
      
      sb.Append("  CustomId: ").Append(this.CustomId).Append("\n");
      
      sb.Append("  Amount: ").Append(this.Amount).Append("\n");
      
      sb.Append("  TotalAmount: ").Append(this.TotalAmount).Append("\n");
      
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