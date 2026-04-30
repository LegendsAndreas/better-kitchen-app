namespace RecipesApi.Services;

public class ApiService
{
    private HttpClient _httpClient;
    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> TestConnection()
    {
        try
        {
            var response = await _httpClient.GetAsync("test");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "Error testing connection: " + e.Message;
        }
    }
}