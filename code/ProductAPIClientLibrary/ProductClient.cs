
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
//using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ProductAPIClientLibrary
{
    public enum AUTHSTATUS { NONE,OK,INVALID,FAILED }


    public static class ProductClient
    {
        static public string baseWebAddress = "https://localhost:44377/";
        static public string UserToken = "";
        static public AUTHSTATUS UserStatus = AUTHSTATUS.NONE;
        static public string IgdbUserToken = "YOUR TOKEN GOES HERE";
        static public List<Product> getProducts()
            {
            using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var response = client.GetAsync(baseWebAddress + "Products").Result;
                    var resultContent = response.Content.ReadAsAsync<List<Product>>(
                        new[] { new JsonMediaTypeFormatter() }).Result;
                    return resultContent;
                }
            }

        static public dynamic getExtGame(int gameID)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("user-key", IgdbUserToken);
                var response = client.GetAsync("https://api-v3.igdb.com/games/" + gameID.ToString() + "/?fields=name,summary,cover").Result;
                var resultContent = response.Content.ReadAsAsync<JToken>(
                    new[] { new JsonMediaTypeFormatter() }).Result;


                var jname = resultContent.Children()["name"].Values<string>().FirstOrDefault();
                var jsummary = resultContent.Children()["summary"].Values<string>().FirstOrDefault();
                // url is nested in cover object
                var jcover = resultContent.Children()["cover"].Values<string>().FirstOrDefault();
                ExternalGameObject eobj = 
                                        new ExternalGameObject
                                        {
                                            Name = jname,
                                            Summary = jsummary,
                                            Cover = jcover
                                        };


                
                return eobj;
            }
        }

        // Post a score for the current player  
        
        static public bool PostProduct(Product p)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string stringData = JsonConvert.SerializeObject(p);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                var response = client.PostAsync(baseWebAddress + "Products", contentData).Result;
                if (response.IsSuccessStatusCode)
                    return true;
                return false;
            }

        }
    static public bool login(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new LoginViewModel {Username = username, Password = password, RememberMe = true};
                string stringData = JsonConvert.SerializeObject(content);
                    var contentData = new StringContent(stringData,
                                                            Encoding.UTF8, "application/json");
                var result = client.PostAsync(baseWebAddress + "Token", contentData).Result;
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
                        return true;
                    }
                    else
                    {
                        UserToken = "Invalid Login" ;
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
        }

        

}
}
