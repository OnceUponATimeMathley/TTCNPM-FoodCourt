using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FoodCourt.App.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace FoodCourt.App.Services
{
    public static class APIService
    {
        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json,Encoding.UTF8,"application/json");

            var response = await httpClient.PostAsync(AppSettings.APIUrl+ "/api/Accounts/Register", content);

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public static async Task<bool> Login(string email, string password)
        {
            var login = new Login()
            {
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(AppSettings.APIUrl + "/api/Accounts/Login", content);
            if (!response.IsSuccessStatusCode)
                return false;

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.Set("accessToken", result.access_token);
            Preferences.Set("userId", result.user_Id);
            Preferences.Set("userName", result.user_name);
            return true;
        }

    }
}
