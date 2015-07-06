namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class Evidence {
    
    /* Country detected from SMS token */
    [DataMember(Name="by_token", EmitDefaultValue=false)]
    public EvidenceSchema ByToken { get; set; }

    
    /* Country detected by credit card number prefix */
    [DataMember(Name="by_cc", EmitDefaultValue=false)]
    public EvidenceSchema ByCc { get; set; }

    
    /* Used when merchant uses 2003 EU VAT rules. */
    [DataMember(Name="by_2003_rules", EmitDefaultValue=false)]
    public EvidenceSchema By2003Rules { get; set; }

    
    /* Country forced by paramters */
    [DataMember(Name="forced", EmitDefaultValue=false)]
    public EvidenceSchema Forced { get; set; }

    
    /* Country detected by payment method. */
    [DataMember(Name="by_payment_method", EmitDefaultValue=false)]
    public EvidenceSchema ByPaymentMethod { get; set; }

    
    /* Country detected by IP */
    [DataMember(Name="by_ip", EmitDefaultValue=false)]
    public EvidenceSchema ByIp { get; set; }

    
    /* Country guessed from IP due to lack of other evidence */
    [DataMember(Name="guessed_from_ip", EmitDefaultValue=false)]
    public EvidenceSchema GuessedFromIp { get; set; }

    
    /* Additional evidence held by the merchant. Can be used only with a private token. */
    [DataMember(Name="other_commercially_relevant_info", EmitDefaultValue=false)]
    public EvidenceSchema OtherCommerciallyRelevantInfo { get; set; }

    
    /* Country detected by billing country code */
    [DataMember(Name="by_billing", EmitDefaultValue=false)]
    public EvidenceSchema ByBilling { get; set; }

    
    /* Country detected from EU TAX number */
    [DataMember(Name="by_tax_number", EmitDefaultValue=false)]
    public EvidenceSchema ByTaxNumber { get; set; }

    
    /* Self declared country as evidence. Requires merchant setting to be active. */
    [DataMember(Name="self_declaration", EmitDefaultValue=false)]
    public EvidenceSchema SelfDeclaration { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Evidence {\n");
      
      sb.Append("  ByToken: ").Append(this.ByToken).Append("\n");
      
      sb.Append("  ByCc: ").Append(this.ByCc).Append("\n");
      
      sb.Append("  By2003Rules: ").Append(this.By2003Rules).Append("\n");
      
      sb.Append("  Forced: ").Append(this.Forced).Append("\n");
      
      sb.Append("  ByPaymentMethod: ").Append(this.ByPaymentMethod).Append("\n");
      
      sb.Append("  ByIp: ").Append(this.ByIp).Append("\n");
      
      sb.Append("  GuessedFromIp: ").Append(this.GuessedFromIp).Append("\n");
      
      sb.Append("  OtherCommerciallyRelevantInfo: ").Append(this.OtherCommerciallyRelevantInfo).Append("\n");
      
      sb.Append("  ByBilling: ").Append(this.ByBilling).Append("\n");
      
      sb.Append("  ByTaxNumber: ").Append(this.ByTaxNumber).Append("\n");
      
      sb.Append("  SelfDeclaration: ").Append(this.SelfDeclaration).Append("\n");
      
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