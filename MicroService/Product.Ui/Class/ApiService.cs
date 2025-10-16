using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Product.Ui.Class
{
    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        public async Task<T> GetDataAsync<T>(string baseUrl, string relativeUrl)
        {
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var res = await _client.GetAsync(relativeUrl);
            if (res.IsSuccessStatusCode)
            {
                var response = await res.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return JsonSerializer.Deserialize<T>("", new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task<TResponse> PostDataAsync<TRequest, TResponse>(string baseUrl, string relativeUrl, TRequest data)
        {
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // 🔹 شیء C# رو به JSON تبدیل می‌کنیم
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(relativeUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(res, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            }
            return JsonSerializer.Deserialize<TResponse>("", new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }
    }
}
