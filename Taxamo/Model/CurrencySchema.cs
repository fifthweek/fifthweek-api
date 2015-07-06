namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CurrencySchema {
    
    /* Currency 3-letter ISO code. */
    [DataMember(Name="code", EmitDefaultValue=false)]
    public string Code { get; set; }

    
    /* Number of minor units for currency. */
    [DataMember(Name="minorunits", EmitDefaultValue=false)]
    public int? Minorunits { get; set; }

    
    /* Currency description. */
    [DataMember(Name="description", EmitDefaultValue=false)]
    public string Description { get; set; }

    
    /* Currency 3-letter ISO code. */
    [DataMember(Name="isocode", EmitDefaultValue=false)]
    public string Isocode { get; set; }

    
    /* Currency iso numeric code. */
    [DataMember(Name="isonum", EmitDefaultValue=false)]
    public int? Isonum { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CurrencySchema {\n");
      
      sb.Append("  Code: ").Append(this.Code).Append("\n");
      
      sb.Append("  Minorunits: ").Append(this.Minorunits).Append("\n");
      
      sb.Append("  Description: ").Append(this.Description).Append("\n");
      
      sb.Append("  Isocode: ").Append(this.Isocode).Append("\n");
      
      sb.Append("  Isonum: ").Append(this.Isonum).Append("\n");
      
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