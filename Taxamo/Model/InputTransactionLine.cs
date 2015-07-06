namespace Taxamo.Model {
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class InputTransactionLine {
    
    /* Product type, according to dictionary /dictionaries/product_types.  */
    [DataMember(Name="product_type", EmitDefaultValue=false)]
    public string ProductType { get; set; }

    
    /* Date of supply in yyyy-MM-dd format. */
    [DataMember(Name="supply_date", EmitDefaultValue=false)]
    public string SupplyDate { get; set; }

    
    /* Unit price. */
    [DataMember(Name="unit_price", EmitDefaultValue=false)]
    public double? UnitPrice { get; set; }

    
    /* Unit of measure. */
    [DataMember(Name="unit_of_measure", EmitDefaultValue=false)]
    public string UnitOfMeasure { get; set; }

    
    /* Quantity Defaults to 1. */
    [DataMember(Name="quantity", EmitDefaultValue=false)]
    public double? Quantity { get; set; }

    
    /* Custom fields, stored as key-value pairs. This property is not processed and used mostly with Taxamo-built helpers. */
    [DataMember(Name="custom_fields", EmitDefaultValue=false)]
    public List<CustomFields> CustomFields { get; set; }

    
    /* Generated line key. */
    [DataMember(Name="line_key", EmitDefaultValue=false)]
    public string LineKey { get; set; }

    
    /* Tax name, calculated by taxamo.  Can be overwritten when informative field is true. */
    [DataMember(Name="tax_name", EmitDefaultValue=false)]
    public string TaxName { get; set; }

    
    /* Internal product code, used for invoicing for example. */
    [DataMember(Name="product_code", EmitDefaultValue=false)]
    public string ProductCode { get; set; }

    
    /* Amount. Required if total amount is not provided. */
    [DataMember(Name="amount", EmitDefaultValue=false)]
    public double? Amount { get; set; }

    
    /* Custom id, provided by ecommerce software. */
    [DataMember(Name="custom_id", EmitDefaultValue=false)]
    public string CustomId { get; set; }

    
    /* If the line is provided for informative purposes. Such line must have :tax-rate and optionally :tax-name - if not, API validation will fail for this line. */
    [DataMember(Name="informative", EmitDefaultValue=false)]
    public bool? Informative { get; set; }

    
    /* Tax rate, calculated by taxamo. Must be provided when informative field is true. */
    [DataMember(Name="tax_rate", EmitDefaultValue=false)]
    public double? TaxRate { get; set; }

    
    /* Total amount. Required if amount is not provided. */
    [DataMember(Name="total_amount", EmitDefaultValue=false)]
    public double? TotalAmount { get; set; }

    
    /* Line contents description. */
    [DataMember(Name="description", EmitDefaultValue=false)]
    public string Description { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class InputTransactionLine {\n");
      
      sb.Append("  ProductType: ").Append(this.ProductType).Append("\n");
      
      sb.Append("  SupplyDate: ").Append(this.SupplyDate).Append("\n");
      
      sb.Append("  UnitPrice: ").Append(this.UnitPrice).Append("\n");
      
      sb.Append("  UnitOfMeasure: ").Append(this.UnitOfMeasure).Append("\n");
      
      sb.Append("  Quantity: ").Append(this.Quantity).Append("\n");
      
      sb.Append("  CustomFields: ").Append(this.CustomFields).Append("\n");
      
      sb.Append("  LineKey: ").Append(this.LineKey).Append("\n");
      
      sb.Append("  TaxName: ").Append(this.TaxName).Append("\n");
      
      sb.Append("  ProductCode: ").Append(this.ProductCode).Append("\n");
      
      sb.Append("  Amount: ").Append(this.Amount).Append("\n");
      
      sb.Append("  CustomId: ").Append(this.CustomId).Append("\n");
      
      sb.Append("  Informative: ").Append(this.Informative).Append("\n");
      
      sb.Append("  TaxRate: ").Append(this.TaxRate).Append("\n");
      
      sb.Append("  TotalAmount: ").Append(this.TotalAmount).Append("\n");
      
      sb.Append("  Description: ").Append(this.Description).Append("\n");
      
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