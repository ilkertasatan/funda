using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Funda.Assignment.Api.UseCases.V1.EstateAgents.GetPropertiesByLocation
{
    public class GetPropertiesByLocationResponse
    {
        [JsonPropertyName("makelaar_id")]
        public int EstateAgentId { get; set; }
        
        [JsonPropertyName("makelaar_naam")]
        public string EstateAgentName { get; set; }
        
        [JsonPropertyName("objects")]
        public IEnumerable<PropertyResponse> Properties { get; set; }
    }

    public class PropertyResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("adres")]
        public string Address { get; set; }
        
        [JsonPropertyName("is_verkocht_of_verhuurd")]
        public bool RentedOrSold { get; set; }
    }
}