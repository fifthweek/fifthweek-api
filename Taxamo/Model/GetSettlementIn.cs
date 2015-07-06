namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class GetSettlementIn {
    
    /* Output format. 'csv' value is accepted as well */
    [DataMember(Name="format", EmitDefaultValue=false)]
    public string Format { get; set; }

    
    /* MOSS country code, used to determine currency. If ommited, merchant default setting is used. */
    [DataMember(Name="moss_country_code", EmitDefaultValue=false)]
    public string MossCountryCode { get; set; }

    
    /* MOSS-assigned tax ID - if not provided, merchant's national tax number will be used. */
    [DataMember(Name="moss_tax_id", EmitDefaultValue=false)]
    public string MossTaxId { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class GetSettlementIn {\n");
      
      sb.Append("  Format: ").Append(this.Format).Append("\n");
      
      sb.Append("  MossCountryCode: ").Append(this.MossCountryCode).Append("\n");
      
      sb.Append("  MossTaxId: ").Append(this.MossTaxId).Append("\n");
      
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