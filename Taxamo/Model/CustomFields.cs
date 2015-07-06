namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CustomFields {
    
    /* Field's key. */
    [DataMember(Name="key", EmitDefaultValue=false)]
    public string Key { get; set; }

    
    /* Field's value. */
    [DataMember(Name="value", EmitDefaultValue=false)]
    public string Value { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CustomFields {\n");
      
      sb.Append("  Key: ").Append(this.Key).Append("\n");
      
      sb.Append("  Value: ").Append(this.Value).Append("\n");
      
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