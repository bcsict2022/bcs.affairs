using app.bcs.affairs.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;

namespace app.bcs.affairs.APIServices
{
    public interface IBCSAffairsService
    {
        public static T GetObject<T>() where T : new()
        {
            return new T();
        }
        Task<string> GetAsync(string id);
        Task<T> GetAllAsync<T>(string url);
        Task<T> GetAllByIdAsync<T>(string url, string id);
        Task<HttpResponseMessage> EditAsync<T>(string url, object requestData);
        Task<HttpResponseMessage> DeleteAsync(string url, string id);
        Task<HttpResponseMessage> CreateAsync(string url, object requestData);
        Task<T> GetTypedObjectAsync<T>(string url, object requestData);
    }

   
    public class BCSAffairsService : IBCSAffairsService
    {
        private HttpClient _httpClient;

        public BCSAffairsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync(string id)
        {
            var apiKey = "";

            string apiURL = $"{apiKey}&q={id}";
            var response = await _httpClient.GetAsync(apiURL);
            return  await response.Content.ReadAsStringAsync();           
        }
        public async Task<T> GetAllAsync<T>(string url)
        {
            try
            {
                var result = default(T);
                string apiURL = $"{url}";
                var response = await _httpClient.GetAsync(apiURL);
                var content = response.Content == null
                                   ? null
                                   : await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(content);
                }
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<T> GetAllByIdAsync<T>(string url, string id)
        {
            try
            {
                var result = default(T);
                //string apiURL = $"{url}+{id}";
                string apiURL = $"{url}{id}";

                var response = await _httpClient.GetAsync(apiURL);
                var content = response.Content == null
                                   ? null
                                   : await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(content);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HttpResponseMessage> CreateAsync(string url, object requestData)
        {
            try
            {
                string apiURL = $"{url}";

                StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                using (var response = await _httpClient.PostAsync(apiURL, content))
                {
                    return response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HttpResponseMessage> EditAsync<T>(string url, object requestData)
        {
            try
            {
                string apiURL = $"{url}";
                StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PutAsync(apiURL, content))
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HttpResponseMessage> DeleteAsync(string url, string id)
        {
            try
            {
                string apiURL = $"{url}+{id}";
                using (var response = await _httpClient.DeleteAsync(apiURL))
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetTypedObjectAsync<T>(string url, object requestData)
        {
            try
            {
                var result = default(T);
               
                string apiURL = $"{url}";

                StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                using (var response = await _httpClient.PostAsync(apiURL, content))
                {
                    var returnedcontent = response.Content == null
                                   ? null
                                   : await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<T>(returnedcontent);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
