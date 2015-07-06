namespace Taxamo.Model {
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CountrySchema {
    
    /* Three letter ISO country code. */
    [DataMember(Name="code_long", EmitDefaultValue=false)]
    public string CodeLong { get; set; }

    
    /* Country ISO 3-digit code. */
    [DataMember(Name="codenum", EmitDefaultValue=false)]
    public string Codenum { get; set; }

    
    /* List of currencies. */
    [DataMember(Name="currency", EmitDefaultValue=false)]
    public List<string> Currency { get; set; }

    
    /* True if tax calculation supported for this country. */
    [DataMember(Name="tax_supported", EmitDefaultValue=false)]
    public bool? TaxSupported { get; set; }

    
    /* Country name. */
    [DataMember(Name="name", EmitDefaultValue=false)]
    public string Name { get; set; }

    
    /* Country ISO 3-digit code. */
    [DataMember(Name="ccn3", EmitDefaultValue=false)]
    public string Ccn3 { get; set; }

    
    /* Three letter ISO country code. */
    [DataMember(Name="cca3", EmitDefaultValue=false)]
    public string Cca3 { get; set; }

    
    /* List of phone number calling codes. */
    [DataMember(Name="callingCode", EmitDefaultValue=false)]
    public List<string> CallingCode { get; set; }

    
    /* VAT number country code. Important for Greece. */
    [DataMember(Name="tax_number_country_code", EmitDefaultValue=false)]
    public string TaxNumberCountryCode { get; set; }

    
    /* Two letter ISO country code. */
    [DataMember(Name="code", EmitDefaultValue=false)]
    public string Code { get; set; }

    
    /* Two letter ISO country code. */
    [DataMember(Name="cca2", EmitDefaultValue=false)]
    public string Cca2 { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CountrySchema {\n");
      
      sb.Append("  CodeLong: ").Append(this.CodeLong).Append("\n");
      
      sb.Append("  Codenum: ").Append(this.Codenum).Append("\n");
      
      sb.Append("  Currency: ").Append(this.Currency).Append("\n");
      
      sb.Append("  TaxSupported: ").Append(this.TaxSupported).Append("\n");
      
      sb.Append("  Name: ").Append(this.Name).Append("\n");
      
      sb.Append("  Ccn3: ").Append(this.Ccn3).Append("\n");
      
      sb.Append("  Cca3: ").Append(this.Cca3).Append("\n");
      
      sb.Append("  CallingCode: ").Append(this.CallingCode).Append("\n");
      
      sb.Append("  TaxNumberCountryCode: ").Append(this.TaxNumberCountryCode).Append("\n");
      
      sb.Append("  Code: ").Append(this.Code).Append("\n");
      
      sb.Append("  Cca2: ").Append(this.Cca2).Append("\n");
      
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