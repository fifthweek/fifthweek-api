namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class LocateGivenIPOut {
    
    /* Remote IP address. */
    [DataMember(Name="remote_addr", EmitDefaultValue=false)]
    public string RemoteAddr { get; set; }

    
    /* Detected country code. */
    [DataMember(Name="country_code", EmitDefaultValue=false)]
    public string CountryCode { get; set; }

    
    /* Detected country details */
    [DataMember(Name="country", EmitDefaultValue=false)]
    public Country Country { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class LocateGivenIPOut {\n");
      
      sb.Append("  RemoteAddr: ").Append(this.RemoteAddr).Append("\n");
      
      sb.Append("  CountryCode: ").Append(this.CountryCode).Append("\n");
      
      sb.Append("  Country: ").Append(this.Country).Append("\n");
      
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