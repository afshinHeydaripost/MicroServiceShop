using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace Helper;



public static class ApiService
{
    public static async Task<TResponse> GetData<TResponse>(this string baseUrl, string relativeUrl)
    {
        string response = "";
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(relativeUrl);
                if (Res.IsSuccessStatusCode)
                {
                    response = Res.Content.ReadAsStringAsync().Result;
                }
            }
            return JsonConvert.DeserializeObject<TResponse>(response);
        }
        catch (Exception e)
        {
            return JsonConvert.DeserializeObject<TResponse>("");
        }
    }

    public static TResponse PostData<TRequest, TResponse>(this string url, TRequest data)
    {
        string response = "";
        try
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var request = client.PostAsync(url, content);
                response = request.Result.Content.ReadAsStringAsync().Result;
            }
            return JsonConvert.DeserializeObject<TResponse>(response);
        }
        catch (Exception e)
        {
            return JsonConvert.DeserializeObject<TResponse>("");
        }
    }

    public static TResponse SendAuthHeaderAndPostData<TRequest, TResponse>(this string url, TRequest data, string token)
    {
        string response = "";
        try
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var request = client.PostAsync(url, content);
                response = request.Result.Content.ReadAsStringAsync().Result;

            }
            return JsonConvert.DeserializeObject<TResponse>(response);
        }
        catch (Exception e)
        {
            return JsonConvert.DeserializeObject<TResponse>("");
        }
    }

    public static async Task<TResponse> SendAuthHeaderAndGetData<TResponse>(this string baseUrl, string relUrl, string token)
    {
        if (string.IsNullOrEmpty(token)) {
            return JsonConvert.DeserializeObject<TResponse>("");
        }
        string response = "";
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                relUrl = string.Format(relUrl);
                HttpResponseMessage Res = await client.GetAsync(relUrl);
                if (Res.IsSuccessStatusCode)
                {
                    response = Res.Content.ReadAsStringAsync().Result;
                }
            }
            return JsonConvert.DeserializeObject<TResponse>(response);
        }
        catch (Exception e)
        {
            return JsonConvert.DeserializeObject<TResponse>("");
        }
    }
}