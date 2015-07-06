namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class EvidenceSchema {
    
    /* If the evidence was used to match the actual country. */
    [DataMember(Name="used", EmitDefaultValue=false)]
    public bool? Used { get; set; }

    
    /* Country code that was resolved using this evidence. */
    [DataMember(Name="resolved_country_code", EmitDefaultValue=false)]
    public string ResolvedCountryCode { get; set; }

    
    /* Type of evidence. */
    [DataMember(Name="evidence_type", EmitDefaultValue=false)]
    public string EvidenceType { get; set; }

    
    /* Value provided as evidence - for example IP address. */
    [DataMember(Name="evidence_value", EmitDefaultValue=false)]
    public string EvidenceValue { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class EvidenceSchema {\n");
      
      sb.Append("  Used: ").Append(this.Used).Append("\n");
      
      sb.Append("  ResolvedCountryCode: ").Append(this.ResolvedCountryCode).Append("\n");
      
      sb.Append("  EvidenceType: ").Append(this.EvidenceType).Append("\n");
      
      sb.Append("  EvidenceValue: ").Append(this.EvidenceValue).Append("\n");
      
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