namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class C {
    
    /* Date for stats in yyyy-MM-dd'T'hh:mm:ss'Z' format. */
    [DataMember(Name="day_raw", EmitDefaultValue=false)]
    public string DayRaw { get; set; }

    
    /* Transaction count. */
    [DataMember(Name="value", EmitDefaultValue=false)]
    public double? Value { get; set; }

    
    /* Transaction status (C or N). */
    [DataMember(Name="status", EmitDefaultValue=false)]
    public string Status { get; set; }

    
    /* Date for stats in yyyy-MM-dd format. */
    [DataMember(Name="day", EmitDefaultValue=false)]
    public string Day { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class C {\n");
      
      sb.Append("  DayRaw: ").Append(this.DayRaw).Append("\n");
      
      sb.Append("  Value: ").Append(this.Value).Append("\n");
      
      sb.Append("  Status: ").Append(this.Status).Append("\n");
      
      sb.Append("  Day: ").Append(this.Day).Append("\n");
      
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