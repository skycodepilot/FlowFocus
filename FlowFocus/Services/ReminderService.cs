using System.Net.Http.Json;

namespace FlowFocus.Services;

public class ReminderService
{
    private readonly HttpClient _http;

    public ReminderService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> GetReminderAsync()
    {
        try
        {
            var result = await _http.GetFromJsonAsync<BoredActivity>(
                "https://www.boredapi.com/api/activity");

            return result?.Activity ?? "Time to stretch!";
        }
        catch
        {
            return "Take a quick stretch break!";
        }
    }

    private class BoredActivity
    {
        public string Activity { get; set; } = "";
    }
}
