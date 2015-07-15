namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class AdditionalCurrency {
    
    /* 3-letter ISO currency code. */
    [DataMember(Name="currency_code", EmitDefaultValue=false)]
    public string CurrencyCode { get; set; }

    
    /* Amount (w/o TAX) in designated currency. */
    [DataMember(Name="amount", EmitDefaultValue=false)]
    public decimal? Amount { get; set; }

    
    /* Tax amount in designated currency. */
    [DataMember(Name="tax_amount", EmitDefaultValue=false)]
    public decimal? TaxAmount { get; set; }

    
    /* Foreign exchange rate used in calculation */
    [DataMember(Name="fx_rate", EmitDefaultValue=false)]
    public decimal? FxRate { get; set; }

    
    /* Total amount in designated currency. */
    [DataMember(Name="total_amount", EmitDefaultValue=false)]
    public decimal? TotalAmount { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class AdditionalCurrency {\n");
      
      sb.Append("  CurrencyCode: ").Append(this.CurrencyCode).Append("\n");
      
      sb.Append("  Amount: ").Append(this.Amount).Append("\n");
      
      sb.Append("  TaxAmount: ").Append(this.TaxAmount).Append("\n");
      
      sb.Append("  FxRate: ").Append(this.FxRate).Append("\n");
      
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