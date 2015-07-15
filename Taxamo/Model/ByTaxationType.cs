namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ByTaxationType {
    
    /* Number of taxed transactions. */
    [DataMember(Name="taxed_count", EmitDefaultValue=false)]
    public decimal? TaxedCount { get; set; }

    
    /* Number of tax deducted transactions. */
    [DataMember(Name="deducted_count", EmitDefaultValue=false)]
    public decimal? DeductedCount { get; set; }

    
    /* Total number of transactions */
    [DataMember(Name="transactions_count", EmitDefaultValue=false)]
    public decimal? TransactionsCount { get; set; }

    
    /* Total EU B2B transaction count. */
    [DataMember(Name="eu_b2b", EmitDefaultValue=false)]
    public int? EuB2b { get; set; }

    
    /* Total EU Taxed transaction count. */
    [DataMember(Name="eu_taxed", EmitDefaultValue=false)]
    public int? EuTaxed { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ByTaxationType {\n");
      
      sb.Append("  TaxedCount: ").Append(this.TaxedCount).Append("\n");
      
      sb.Append("  DeductedCount: ").Append(this.DeductedCount).Append("\n");
      
      sb.Append("  TransactionsCount: ").Append(this.TransactionsCount).Append("\n");
      
      sb.Append("  EuB2b: ").Append(this.EuB2b).Append("\n");
      
      sb.Append("  EuTaxed: ").Append(this.EuTaxed).Append("\n");
      
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