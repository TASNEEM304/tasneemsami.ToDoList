using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net;
using System;

namespace TasneemSami.ToDoList.Tests.Integration
{
    public class UserTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<string> GetAdminTokenAsync()
        {
            var login = new { UserName = "admin", Password = "admin123" };
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth/login", content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<dynamic>(responseBody).token;

            return (string)token;
        }

        [Fact]
        public async Task AddUser_ShouldReturnSuccess()
        {
            var token = await GetAdminTokenAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var newUser = new { UserName = "testuser", Password = "123456", Role = 2 };
            var content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth/register", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Content: " + responseContent);

            response.EnsureSuccessStatusCode(); 
        }

        [Fact]
        public async Task Login_ShouldReturnToken()
        {
            var login = new { UserName = "admin", Password = "admin123" };
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth/login", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.Contains("token", responseBody.ToLower());
        }

        [Fact]
        public async Task Admin_ShouldHaveAccessToProtectedRoute()
        {
            var token = await GetAdminTokenAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync("/api/admin/only");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task NormalUser_ShouldNotAccess_AdminOnly()
        {
            var adminToken = await GetAdminTokenAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

            var newUser = new { UserName = "normaluser", Password = "123456", Role = 2 };
            var registerContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var registerResponse = await _client.PostAsync("/api/auth/register", registerContent);
            registerResponse.EnsureSuccessStatusCode();

            var login = new { UserName = "normaluser", Password = "123456" };
            var loginContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var loginResponse = await _client.PostAsync("/api/auth/login", loginContent);
            loginResponse.EnsureSuccessStatusCode();

            var loginBody = await loginResponse.Content.ReadAsStringAsync();
            var userToken = JsonConvert.DeserializeObject<dynamic>(loginBody).token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)userToken);
            var response = await _client.GetAsync("/api/admin/only");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GetAllTasks_ShouldWorkForAnyone()
        {
            _client.DefaultRequestHeaders.Authorization = null;

            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/task/getall", content);
            response.EnsureSuccessStatusCode();
        }
    }

}
