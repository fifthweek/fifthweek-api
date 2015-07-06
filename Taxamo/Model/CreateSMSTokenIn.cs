namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CreateSMSTokenIn {
    
    /* Two letter ISO country code. */
    [DataMember(Name="country_code", EmitDefaultValue=false)]
    public string CountryCode { get; set; }

    
    /* Recipient phone number. */
    [DataMember(Name="recipient", EmitDefaultValue=false)]
    public string Recipient { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CreateSMSTokenIn {\n");
      
      sb.Append("  CountryCode: ").Append(this.CountryCode).Append("\n");
      
      sb.Append("  Recipient: ").Append(this.Recipient).Append("\n");
      
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