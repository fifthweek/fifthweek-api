namespace Taxamo.Model {
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class InvoiceAddress {
    
    /* Freeform address. Use when structured data is not available. Will be used in SAF-MOSS file if other fields are not provided. */
    [DataMember(Name="freeform_address", EmitDefaultValue=false)]
    public string FreeformAddress { get; set; }

    
    /* Building number. */
    [DataMember(Name="building_number", EmitDefaultValue=false)]
    public string BuildingNumber { get; set; }

    
    /*  Street name. */
    [DataMember(Name="street_name", EmitDefaultValue=false)]
    public string StreetName { get; set; }

    
    /* Address details - for example apartament number. */
    [DataMember(Name="address_detail", EmitDefaultValue=false)]
    public string AddressDetail { get; set; }

    
    /* City name. */
    [DataMember(Name="city", EmitDefaultValue=false)]
    public string City { get; set; }

    
    /* Postal code. */
    [DataMember(Name="postal_code", EmitDefaultValue=false)]
    public string PostalCode { get; set; }

    
    /* Region. */
    [DataMember(Name="region", EmitDefaultValue=false)]
    public string Region { get; set; }

    
    /* 2-letter ISO country code. */
    [DataMember(Name="country", EmitDefaultValue=false)]
    public string Country { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class InvoiceAddress {\n");
      
      sb.Append("  FreeformAddress: ").Append(this.FreeformAddress).Append("\n");
      
      sb.Append("  BuildingNumber: ").Append(this.BuildingNumber).Append("\n");
      
      sb.Append("  StreetName: ").Append(this.StreetName).Append("\n");
      
      sb.Append("  AddressDetail: ").Append(this.AddressDetail).Append("\n");
      
      sb.Append("  City: ").Append(this.City).Append("\n");
      
      sb.Append("  PostalCode: ").Append(this.PostalCode).Append("\n");
      
      sb.Append("  Region: ").Append(this.Region).Append("\n");
      
      sb.Append("  Country: ").Append(this.Country).Append("\n");
      
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