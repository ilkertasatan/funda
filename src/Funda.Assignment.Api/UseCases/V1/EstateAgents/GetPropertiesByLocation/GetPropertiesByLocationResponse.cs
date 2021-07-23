using System.Text.Json.Serialization;

namespace Funda.Assignment.Api.UseCases.V1.EstateAgents.GetPropertiesByLocation
{
    public class GetPropertiesByLocationResponse
    {
        [JsonPropertyName("makelaar_id")]
        public int EstateAgentId { get; set; }
        
        [JsonPropertyName("makelaar_naam")]
        public string EstateAgentName { get; set; }
    }
}