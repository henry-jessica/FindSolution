using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataServices
{


    public enum AUTHSTATUS { NONE, OK, INVALID, FAILED }
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        public HttpClientService(HttpClient httpClient,
            ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public Token Token_held { get; private set; }
        public string UserToken { get; set; }
        public AUTHSTATUS UserStatus { get; set; }

        public async Task<List<T>> getCollection<T>(string EndPoint)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            if (Token_held == null)
                Token_held = await _localStorageService.GetItem<Token>("token");
            // if we are logged in with an existing token in local storage then use it
            if (Token_held != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token_held.AccessToken);
                UserStatus = AUTHSTATUS.NONE;
            }
            // Otherwise try call without authentication header
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // New Blazor Get
            var response = await _httpClient.GetFromJsonAsync<List<T>>(EndPoint);

            return response;
        }

        public async Task<T> getSingle<T>(string Endpoint, int id)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            if (Token_held == null)
                Token_held = await _localStorageService.GetItem<Token>("token");

            if (Token_held != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token_held.AccessToken);
                UserStatus = AUTHSTATUS.NONE;
            }
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // New Blazor Get
            var response = await _httpClient.GetFromJsonAsync<T>(Endpoint + id.ToString());
            return response;
        }
        public async Task Initialize()
        {
            Token_held = await _localStorageService.GetItem<Token>("token");
        }

        public async Task<Token> GetTokenAsync()
        {
            Token t = await _localStorageService.GetItem<Token>("token");
            if (t != null)
            {
                UserStatus = AUTHSTATUS.NONE;
            }
            return t;
        }
        public async Task<bool> login(string username, string password)
        {

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new LoginViewModel { Username = username, Password = password, RememberMe = true };
            var result = await _httpClient.PostAsJsonAsync("Token", content);
            try
            {
                var resultContent = result.Content.ReadAsAsync<Token>(
                        new[] { new JsonMediaTypeFormatter() }
                        ).Result;
                string ServerError = string.Empty;
                if (!(String.IsNullOrEmpty(resultContent.AccessToken)))
                {
                    Console.WriteLine(resultContent.AccessToken);
                    UserToken = resultContent.AccessToken;
                    UserStatus = AUTHSTATUS.OK;
                    await _localStorageService.SetItem("token", resultContent);
                    return true;
                }
                else
                {
                    UserToken = "Invalid Login";
                    UserStatus = AUTHSTATUS.INVALID;
                    Console.WriteLine("Invalid credentials");
                    return false;
                }
            }
            catch (Exception ex)
            {
                UserStatus = AUTHSTATUS.FAILED;
                UserToken = "Server Error -> " + ex.Message;
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async void Post<T>(string EndPoint, T p)
        {
            if (Token_held == null)
                Token_held = await _localStorageService.GetItem<Token>("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token_held.AccessToken);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            await _httpClient.PutAsJsonAsync(EndPoint, p);

        }

        public async void Put<T>(string EndPoint, T p)
        {
            if (Token_held == null)
                Token_held = await _localStorageService.GetItem<Token>("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token_held.AccessToken);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            await _httpClient.PutAsJsonAsync(EndPoint, p);
        }
    }
}
