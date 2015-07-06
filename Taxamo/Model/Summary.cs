namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class Summary {
    
    /* Quarter that this summary applies to. */
    [DataMember(Name="quarter", EmitDefaultValue=false)]
    public string Quarter { get; set; }

    
    /* Tax amount due in this quarter. */
    [DataMember(Name="tax_amount", EmitDefaultValue=false)]
    public double? TaxAmount { get; set; }

    
    /* In which currency code the settlement was calculated. */
    [DataMember(Name="currency_code", EmitDefaultValue=false)]
    public string CurrencyCode { get; set; }

    
    /* If the quarter isn't closed yet, tax amount is indicative, as we cannot determine FX rate or all transactions yet. */
    [DataMember(Name="indicative", EmitDefaultValue=false)]
    public bool? Indicative { get; set; }

    
    /* Date of ECB FX rate used for conversions in yyyy-MM-dd'T'hh:mm:ss'Z' format. */
    [DataMember(Name="fx_rate_date", EmitDefaultValue=false)]
    public string FxRateDate { get; set; }

    
    /* Tax entity that the tax is due. */
    [DataMember(Name="tax_entity_name", EmitDefaultValue=false)]
    public string TaxEntityName { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Summary {\n");
      
      sb.Append("  Quarter: ").Append(this.Quarter).Append("\n");
      
      sb.Append("  TaxAmount: ").Append(this.TaxAmount).Append("\n");
      
      sb.Append("  CurrencyCode: ").Append(this.CurrencyCode).Append("\n");
      
      sb.Append("  Indicative: ").Append(this.Indicative).Append("\n");
      
      sb.Append("  FxRateDate: ").Append(this.FxRateDate).Append("\n");
      
      sb.Append("  TaxEntityName: ").Append(this.TaxEntityName).Append("\n");
      
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