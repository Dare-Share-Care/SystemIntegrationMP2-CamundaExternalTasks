using System.Text.Json.Serialization;

namespace CamundaService2.Models.Dtos;

public class FetchAndLockDto
{
    [JsonPropertyName("workerId")]
    public string? WorkerId { get; set; }
    [JsonPropertyName("maxTasks")]
    public int MaxTasks { get; set; }
    [JsonPropertyName("topics")]
    public List<TopicDto>? Topics { get; set; }
}