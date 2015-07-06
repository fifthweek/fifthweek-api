namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ListPaymentsIn {
    
    /* Max record count (no more than 100, defaults to 10). */
    [DataMember(Name="limit", EmitDefaultValue=false)]
    public string Limit { get; set; }

    
    /* How many records need to be skipped, defaults to 0. */
    [DataMember(Name="offset", EmitDefaultValue=false)]
    public string Offset { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ListPaymentsIn {\n");
      
      sb.Append("  Limit: ").Append(this.Limit).Append("\n");
      
      sb.Append("  Offset: ").Append(this.Offset).Append("\n");
      
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