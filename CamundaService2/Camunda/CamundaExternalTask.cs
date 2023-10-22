using System.Net;
using System.Text;
using System.Text.Json;
using CamundaService2.Models.Dtos;

namespace CamundaService2.Camunda;

public class CamundaExternalTask
{
    private readonly HttpClient _httpClient;

    public CamundaExternalTask()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task CompleteExternalTask(CompleteExternalTaskDto dto)
    {
        //Lock task worker
        await FetchAndLockExternalTask(dto.Topic);
        
         var url = $"http://localhost:8080/engine-rest/external-task/{dto.Id}/complete";
         var dtoJson = JsonSerializer.Serialize(dto);
         var content = new StringContent(dtoJson, Encoding.UTF8, "application/json");
         var response = await _httpClient.PostAsync(url, content);
         var result = await response.Content.ReadAsStringAsync();
         if (response.StatusCode is HttpStatusCode.OK or HttpStatusCode.NoContent)
         {
             Console.WriteLine("Task completed");
         }
         else
         {
             Console.WriteLine("Error: " + result);
         }
        
        // Unlock task worker
         await UnlockExternalTask(dto.Id);
    }

    private async Task FetchAndLockExternalTask(TopicDto topicDto)
    {
        var dto = new FetchAndLockDto()
        {
            WorkerId = "C#Worker",
            MaxTasks = 1,
            Topics = new List<TopicDto>()
        };
        
        dto.Topics.Add(topicDto);
        
        var url = "http://localhost:8080/engine-rest/external-task/fetchAndLock";
        var dtoJson = JsonSerializer.Serialize(dto);
        var content = new StringContent(dtoJson, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
    }

    private async Task UnlockExternalTask(string id)
    {
        var url = $"http://localhost:8080/engine-rest/external-task/{id}/unlock";
        var response = await _httpClient.PostAsync(url, null);
    }
}