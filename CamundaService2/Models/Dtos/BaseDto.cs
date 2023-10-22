using System.Text.Json.Serialization;

namespace CamundaService2.Models.Dtos;

public class BaseDto
{
    [JsonPropertyName("id")] 
    public string? Id { get; set; }
}