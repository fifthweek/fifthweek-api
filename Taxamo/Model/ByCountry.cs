namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ByCountry {
    
    /* Tax amount */
    [DataMember(Name="value", EmitDefaultValue=false)]
    public decimal? Value { get; set; }

    
    /* Country name */
    [DataMember(Name="tax_country_name", EmitDefaultValue=false)]
    public string TaxCountryName { get; set; }

    
    /* Two letter ISO country code. */
    [DataMember(Name="tax_country_code", EmitDefaultValue=false)]
    public string TaxCountryCode { get; set; }

    
    /* Three-letter ISO currency code. */
    [DataMember(Name="currency_code", EmitDefaultValue=false)]
    public string CurrencyCode { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ByCountry {\n");
      
      sb.Append("  Value: ").Append(this.Value).Append("\n");
      
      sb.Append("  TaxCountryName: ").Append(this.TaxCountryName).Append("\n");
      
      sb.Append("  TaxCountryCode: ").Append(this.TaxCountryCode).Append("\n");
      
      sb.Append("  CurrencyCode: ").Append(this.CurrencyCode).Append("\n");
      
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