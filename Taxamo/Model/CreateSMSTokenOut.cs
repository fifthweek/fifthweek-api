namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CreateSMSTokenOut {
    
    /* Always set to true. Success means that the message has been queued for delivery. In some cases message may be undelivered at the end, e.g. mobile number is blacklisted. API errors are signalled with appropriate error codes. */
    [DataMember(Name="success", EmitDefaultValue=false)]
    public bool? Success { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CreateSMSTokenOut {\n");
      
      sb.Append("  Success: ").Append(this.Success).Append("\n");
      
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