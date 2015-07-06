namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class GetRefundsIn {
    
    /* Output format. 'csv' value is accepted as well */
    [DataMember(Name="format", EmitDefaultValue=false)]
    public string Format { get; set; }

    
    /* MOSS country code, used to determine currency. If ommited, merchant default setting is used. */
    [DataMember(Name="moss_country_code", EmitDefaultValue=false)]
    public string MossCountryCode { get; set; }

    
    /* Take only refunds issued at or after the date. Format: yyyy-MM-dd */
    [DataMember(Name="date_from", EmitDefaultValue=false)]
    public string DateFrom { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class GetRefundsIn {\n");
      
      sb.Append("  Format: ").Append(this.Format).Append("\n");
      
      sb.Append("  MossCountryCode: ").Append(this.MossCountryCode).Append("\n");
      
      sb.Append("  DateFrom: ").Append(this.DateFrom).Append("\n");
      
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