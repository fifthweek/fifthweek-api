namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CreateTransactionIn {
    
    /* Use manual mode, bypassing country detection. Only allowed with private token. This flag allows to use original_transaction_key field */
    [DataMember(Name="manual_mode", EmitDefaultValue=false)]
    public bool? ManualMode { get; set; }

    
    /* Transaction. */
    [DataMember(Name="transaction", EmitDefaultValue=false)]
    public InputTransaction Transaction { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CreateTransactionIn {\n");
      
      sb.Append("  ManualMode: ").Append(this.ManualMode).Append("\n");
      
      sb.Append("  Transaction: ").Append(this.Transaction).Append("\n");
      
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