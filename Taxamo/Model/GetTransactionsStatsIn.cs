namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class GetTransactionsStatsIn {
    
    /* Date from in yyyy-MM format. */
    [DataMember(Name="date_from", EmitDefaultValue=false)]
    public string DateFrom { get; set; }

    
    /* Date to in yyyy-MM format. */
    [DataMember(Name="date_to", EmitDefaultValue=false)]
    public string DateTo { get; set; }

    
    /* Interval. Accepted values are 'day', 'week' and 'month'. */
    [DataMember(Name="interval", EmitDefaultValue=false)]
    public string Interval { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class GetTransactionsStatsIn {\n");
      
      sb.Append("  DateFrom: ").Append(this.DateFrom).Append("\n");
      
      sb.Append("  DateTo: ").Append(this.DateTo).Append("\n");
      
      sb.Append("  Interval: ").Append(this.Interval).Append("\n");
      
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