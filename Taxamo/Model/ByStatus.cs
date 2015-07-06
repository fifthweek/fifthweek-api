namespace Taxamo.Model {
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ByStatus {
    
    /* New transactions */
    [DataMember(Name="N", EmitDefaultValue=false)]
    public List<N> N { get; set; }

    
    /* Confirmed transactions */
    [DataMember(Name="C", EmitDefaultValue=false)]
    public List<C> C { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ByStatus {\n");
      
      sb.Append("  N: ").Append(this.N).Append("\n");
      
      sb.Append("  C: ").Append(this.C).Append("\n");
      
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