namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CreateRefundOut {
    
    /* Total amount, including tax, that was refunded in this call. */
    [DataMember(Name="total_amount", EmitDefaultValue=false)]
    public double? TotalAmount { get; set; }

    
    /* Calculated tax amount, that was refunded in this call. */
    [DataMember(Name="tax_amount", EmitDefaultValue=false)]
    public double? TaxAmount { get; set; }

    
    /* Total amount, including tax, that was refunded for this line. */
    [DataMember(Name="refunded_total_amount", EmitDefaultValue=false)]
    public double? RefundedTotalAmount { get; set; }

    
    /* Total tax amount, that was refunded for this line. */
    [DataMember(Name="refunded_tax_amount", EmitDefaultValue=false)]
    public double? RefundedTaxAmount { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CreateRefundOut {\n");
      
      sb.Append("  TotalAmount: ").Append(this.TotalAmount).Append("\n");
      
      sb.Append("  TaxAmount: ").Append(this.TaxAmount).Append("\n");
      
      sb.Append("  RefundedTotalAmount: ").Append(this.RefundedTotalAmount).Append("\n");
      
      sb.Append("  RefundedTaxAmount: ").Append(this.RefundedTaxAmount).Append("\n");
      
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