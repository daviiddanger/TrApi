using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrSinntecPrueba.Models;
using Xamarin.Essentials;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<bool> RegisterPackageAsync(Data package)
    {
        var token = await SecureStorage.GetAsync("access_token");
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var json = JsonConvert.SerializeObject(package);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://practice.sinntec.com.mx/api/v1/package", content);
        return response.IsSuccessStatusCode;
    }
    public async Task<Data> GetPackageAsync(int packageId)
    {
        var token = await SecureStorage.GetAsync("access_token");
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"https://practice.sinntec.com.mx/api/v1/package/{packageId}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);
            return apiResponse?.data;
        }
        return null;
    }

    public async Task<bool> UpdatePackageAsync(int packageId, Data package)
    {
        var token = await SecureStorage.GetAsync("access_token");
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var json = JsonConvert.SerializeObject(package);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"https://practice.sinntec.com.mx/api/v1/package/{packageId}", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeletePackageAsync(int packageId)
    {
        var token = await SecureStorage.GetAsync("access_token");
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.DeleteAsync($"https://practice.sinntec.com.mx/api/v1/package/{packageId}");
        return response.IsSuccessStatusCode;
    }



}
