namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class GetSettlementStatsByCountryIn {
    
    /* Date from in yyyy-MM format. */
    [DataMember(Name="date_from", EmitDefaultValue=false)]
    public string DateFrom { get; set; }

    
    /* Date to in yyyy-MM format. */
    [DataMember(Name="date_to", EmitDefaultValue=false)]
    public string DateTo { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class GetSettlementStatsByCountryIn {\n");
      
      sb.Append("  DateFrom: ").Append(this.DateFrom).Append("\n");
      
      sb.Append("  DateTo: ").Append(this.DateTo).Append("\n");
      
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