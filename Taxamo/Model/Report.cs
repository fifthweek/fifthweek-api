namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class Report {
    
    /* Tax rate */
    [DataMember(Name="tax_rate", EmitDefaultValue=false)]
    public decimal? TaxRate { get; set; }

    
    /* Amount w/o tax */
    [DataMember(Name="amount", EmitDefaultValue=false)]
    public decimal? Amount { get; set; }

    
    /* Country name */
    [DataMember(Name="country_name", EmitDefaultValue=false)]
    public string CountryName { get; set; }

    
    /* Two letter ISO country code. */
    [DataMember(Name="country_code", EmitDefaultValue=false)]
    public string CountryCode { get; set; }

    
    /* Tax amount */
    [DataMember(Name="tax_amount", EmitDefaultValue=false)]
    public decimal? TaxAmount { get; set; }

    
    /* If true, this line should not be entered into MOSS and is provided for informative purposes only. For example because the country is the same as MOSS registration country and merchant country. */
    [DataMember(Name="skip_moss", EmitDefaultValue=false)]
    public bool? SkipMoss { get; set; }

    
    /* Three-letter ISO currency code. */
    [DataMember(Name="currency_code", EmitDefaultValue=false)]
    public string CurrencyCode { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Report {\n");
      
      sb.Append("  TaxRate: ").Append(this.TaxRate).Append("\n");
      
      sb.Append("  Amount: ").Append(this.Amount).Append("\n");
      
      sb.Append("  CountryName: ").Append(this.CountryName).Append("\n");
      
      sb.Append("  CountryCode: ").Append(this.CountryCode).Append("\n");
      
      sb.Append("  TaxAmount: ").Append(this.TaxAmount).Append("\n");
      
      sb.Append("  SkipMoss: ").Append(this.SkipMoss).Append("\n");
      
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