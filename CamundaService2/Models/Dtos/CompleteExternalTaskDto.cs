using System.Text.Json.Serialization;

namespace CamundaService2.Models.Dtos;

public class CompleteExternalTaskDto : BaseDto
{
    [JsonPropertyName("workerId")]
    public string? WorkerId { get; set; }
    [JsonPropertyName("variables")]
    public Dictionary<string, Dictionary<string, object>>? Variables { get; set; }
    [JsonPropertyName("topic")]
    public TopicDto Topic { get; set; }
}