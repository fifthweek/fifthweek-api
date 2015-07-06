namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class SettlementDailyStatsSchema {
    
    /* B2C transaction count. */
    [DataMember(Name="b2c", EmitDefaultValue=false)]
    public int? B2c { get; set; }

    
    /* Untaxed transaction count. */
    [DataMember(Name="untaxed", EmitDefaultValue=false)]
    public int? Untaxed { get; set; }

    
    /* Total EU Taxed transaction count. */
    [DataMember(Name="eu_taxed", EmitDefaultValue=false)]
    public int? EuTaxed { get; set; }

    
    /* Total EU B2B transaction count. */
    [DataMember(Name="eu_b2b", EmitDefaultValue=false)]
    public int? EuB2b { get; set; }

    
    /* Total transaction count. */
    [DataMember(Name="count", EmitDefaultValue=false)]
    public int? Count { get; set; }

    
    /* Total EU transaction count. */
    [DataMember(Name="eu_total", EmitDefaultValue=false)]
    public int? EuTotal { get; set; }

    
    /* Date for stats in yyyy-MM-dd'T'hh:mm:ss'Z' format. */
    [DataMember(Name="day_raw", EmitDefaultValue=false)]
    public string DayRaw { get; set; }

    
    /* B2B transaction count. */
    [DataMember(Name="b2b", EmitDefaultValue=false)]
    public int? B2b { get; set; }

    
    /* Date for stats in yyyy-MM-dd format. */
    [DataMember(Name="day", EmitDefaultValue=false)]
    public string Day { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class SettlementDailyStatsSchema {\n");
      
      sb.Append("  B2c: ").Append(this.B2c).Append("\n");
      
      sb.Append("  Untaxed: ").Append(this.Untaxed).Append("\n");
      
      sb.Append("  EuTaxed: ").Append(this.EuTaxed).Append("\n");
      
      sb.Append("  EuB2b: ").Append(this.EuB2b).Append("\n");
      
      sb.Append("  Count: ").Append(this.Count).Append("\n");
      
      sb.Append("  EuTotal: ").Append(this.EuTotal).Append("\n");
      
      sb.Append("  DayRaw: ").Append(this.DayRaw).Append("\n");
      
      sb.Append("  B2b: ").Append(this.B2b).Append("\n");
      
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