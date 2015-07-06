namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class GetSettlementSummaryIn {
    
    /* MOSS country code, used to determine currency. If ommited, merchant default setting is used. */
    [DataMember(Name="moss_country_code", EmitDefaultValue=false)]
    public string MossCountryCode { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class GetSettlementSummaryIn {\n");
      
      sb.Append("  MossCountryCode: ").Append(this.MossCountryCode).Append("\n");
      
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