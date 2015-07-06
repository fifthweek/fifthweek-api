namespace Taxamo.Model {
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class GetSettlementOut {
    
    /* Settlement report. */
    [DataMember(Name="report", EmitDefaultValue=false)]
    public List<Report> Report { get; set; }

    
    /* Quarter start date in yyyy-MM-dd'T'hh:mm:ss'Z' format. */
    [DataMember(Name="start_date", EmitDefaultValue=false)]
    public string StartDate { get; set; }

    
    /* Quarter end date in yyyy-MM-dd'T'hh:mm:ss'Z' format. */
    [DataMember(Name="end_date", EmitDefaultValue=false)]
    public string EndDate { get; set; }

    
    /* If the quarter isn't closed yet, tax amount is indicative, as we cannot determine FX rate or all transactions yet. */
    [DataMember(Name="indicative", EmitDefaultValue=false)]
    public bool? Indicative { get; set; }

    
    /* Date of ECB FX rate used for conversions in yyyy-MM-dd'T'hh:mm:ss'Z' format. */
    [DataMember(Name="fx_rate_date", EmitDefaultValue=false)]
    public string FxRateDate { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class GetSettlementOut {\n");
      
      sb.Append("  Report: ").Append(this.Report).Append("\n");
      
      sb.Append("  StartDate: ").Append(this.StartDate).Append("\n");
      
      sb.Append("  EndDate: ").Append(this.EndDate).Append("\n");
      
      sb.Append("  Indicative: ").Append(this.Indicative).Append("\n");
      
      sb.Append("  FxRateDate: ").Append(this.FxRateDate).Append("\n");
      
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