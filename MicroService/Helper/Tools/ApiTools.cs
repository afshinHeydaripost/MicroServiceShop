using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Helper;
public class ApiResult<T>
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public T? Data { get; set; }

    public static ApiResult<T> Ok(T data) => new() { Success = true, Data = data };
    public static ApiResult<T> Fail(string error) => new() { Success = false, Error = error };
}
public static class ApiClient
{
    private static readonly HttpClient _http = new()
    {
        Timeout = TimeSpan.FromSeconds(100)
    };

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static readonly object _lock = new();

    private static void SetToken(string token)
    {
        lock (_lock)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    private static void ClearToken()
    {
        lock (_lock)
        {
            _http.DefaultRequestHeaders.Authorization = null;
        }
    }

    private static StringContent ToJson(object data)
    {
        return new(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json"
        );
    }

    private static async Task<ApiResult<T>> HandleResponse<T>(HttpResponseMessage res)
    {
        string json = await res.Content.ReadAsStringAsync();

        if (!res.IsSuccessStatusCode)
            return ApiResult<T>.Fail($"HTTP {(int)res.StatusCode}: {json}");

        try
        {
            var data = JsonSerializer.Deserialize<T>(json, _jsonOptions);
            return ApiResult<T>.Ok(data!);
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Fail("JSON Parse Error: " + ex.Message);
        }
    }

    // GET بدون توکن
    public static async Task<ApiResult<T>> GetAsync<T>(this string url)
    {
        try
        {
            ClearToken();
            var res = await _http.GetAsync(url);
            return await HandleResponse<T>(res);
        }
        catch (TaskCanceledException)
        {
            return ApiResult<T>.Fail("Timeout Error");
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Fail(ex.Message);
        }
    }

    // GET با توکن
    public static async Task<ApiResult<T>> GetWithTokenAsync<T>(this string url, string token)
    {
        try
        {
            SetToken(token);
            var res = await _http.GetAsync(url);
            return await HandleResponse<T>(res);
        }
        catch (TaskCanceledException)
        {
            return ApiResult<T>.Fail("Timeout Error");
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Fail(ex.Message);
        }
    }

    // POST بدون توکن
    public static async Task<ApiResult<T>> PostAsync<T>(this string url, object body)
    {
        try
        {
            ClearToken();
            var res = await _http.PostAsync(url, ToJson(body));
            return await HandleResponse<T>(res);
        }
        catch (TaskCanceledException)
        {
            return ApiResult<T>.Fail("Timeout Error");
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Fail(ex.Message);
        }
    }

    // POST با توکن
    public static async Task<ApiResult<T>> PostWithTokenAsync<T>(this string url, object body, string token)
    {
        try
        {
            SetToken(token);
            var res = await _http.PostAsync(url, ToJson(body));
            return await HandleResponse<T>(res);
        }
        catch (TaskCanceledException)
        {
            return ApiResult<T>.Fail("Timeout Error");
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Fail(ex.Message);
        }
    }
}



