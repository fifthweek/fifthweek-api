namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ListTransactionsIn {
    
    /* Filtering expression */
    [DataMember(Name="filter_text", EmitDefaultValue=false)]
    public string FilterText { get; set; }

    
    /* Offset */
    [DataMember(Name="offset", EmitDefaultValue=false)]
    public int? Offset { get; set; }

    
    /* Taxamo provided transaction key or custom id */
    [DataMember(Name="key_or_custom_id", EmitDefaultValue=false)]
    public string KeyOrCustomId { get; set; }

    
    /* Three letter ISO currency code. */
    [DataMember(Name="currency_code", EmitDefaultValue=false)]
    public string CurrencyCode { get; set; }

    
    /* Order date to in yyyy-MM-dd format. */
    [DataMember(Name="order_date_to", EmitDefaultValue=false)]
    public string OrderDateTo { get; set; }

    
    /* If true, results are sorted in descending order. */
    [DataMember(Name="sort_reverse", EmitDefaultValue=false)]
    public bool? SortReverse { get; set; }

    
    /* Limit (no more than 1000, defaults to 100). */
    [DataMember(Name="limit", EmitDefaultValue=false)]
    public int? Limit { get; set; }

    
    /* Transaction invoice number. */
    [DataMember(Name="invoice_number", EmitDefaultValue=false)]
    public string InvoiceNumber { get; set; }

    
    /* Comma separated list of of transaction statuses. */
    [DataMember(Name="statuses", EmitDefaultValue=false)]
    public string Statuses { get; set; }

    
    /* Order date from in yyyy-MM-dd format. */
    [DataMember(Name="order_date_from", EmitDefaultValue=false)]
    public string OrderDateFrom { get; set; }

    
    /* Output format - supports 'csv' value for this operation. */
    [DataMember(Name="format", EmitDefaultValue=false)]
    public string Format { get; set; }

    
    /* Two letter ISO tax country code. */
    [DataMember(Name="tax_country_code", EmitDefaultValue=false)]
    public string TaxCountryCode { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ListTransactionsIn {\n");
      
      sb.Append("  FilterText: ").Append(this.FilterText).Append("\n");
      
      sb.Append("  Offset: ").Append(this.Offset).Append("\n");
      
      sb.Append("  KeyOrCustomId: ").Append(this.KeyOrCustomId).Append("\n");
      
      sb.Append("  CurrencyCode: ").Append(this.CurrencyCode).Append("\n");
      
      sb.Append("  OrderDateTo: ").Append(this.OrderDateTo).Append("\n");
      
      sb.Append("  SortReverse: ").Append(this.SortReverse).Append("\n");
      
      sb.Append("  Limit: ").Append(this.Limit).Append("\n");
      
      sb.Append("  InvoiceNumber: ").Append(this.InvoiceNumber).Append("\n");
      
      sb.Append("  Statuses: ").Append(this.Statuses).Append("\n");
      
      sb.Append("  OrderDateFrom: ").Append(this.OrderDateFrom).Append("\n");
      
      sb.Append("  Format: ").Append(this.Format).Append("\n");
      
      sb.Append("  TaxCountryCode: ").Append(this.TaxCountryCode).Append("\n");
      
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