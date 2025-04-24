using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System;

namespace TasneemSami.ToDoList.Tests.Integration;

public class UserAndTaskTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserAndTaskTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
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
    public async Task Admin_ShouldAccessProtectedRoute()
    {
        var token = await GetTokenAsync("admin", "admin123");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync("/api/admin/only");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task NormalUser_CannotAddTask_ShouldReturnForbidden()
    {
        var token = await GetTokenAsync("user", "user123");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var task = new
        {
            Title = "Admin Task",
            Description = "Created by admin",
            DueDate = new DateTime(2025, 12, 31),
            IsCompleted = false,
            Category = 1,
            Priority = 1,
            UserId = 9  
        };

       
        var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/task/insert", content);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task NormalUser_CannotEditTask_ShouldReturnForbidden()
    {
        var token = await GetTokenAsync("user", "user123");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var updatedTask = new
        {
            Title = "Admin Task",
            Description = "Created by admin",
            DueDate = new DateTime(2025, 12, 31),
            IsCompleted = false,
            Category = 1,
            Priority = 1,
            UserId = 1  // تأكد أن المستخدم ID=1 موجود (مثل admin)
        };
       
        var content = new StringContent(JsonConvert.SerializeObject(updatedTask), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/task/edit", content);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task NormalUser_CannotDeleteTask_ShouldReturnForbidden()
    {
        var token = await GetTokenAsync("user", "user123");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.DeleteAsync("/api/task/delete?id=1");
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Admin_CanInsertTask()
    {

        var token = await GetTokenAsync("admin", "admin123");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var newTask = new
        {
            Title = "Admin Task",
            Description = "Created by admin",
            DueDate = new DateTime(2025, 12, 31),
            IsCompleted = false,
            Category = 1,
            Priority = 1,
            UserId = 9
        };

        
        var content = new StringContent(JsonConvert.SerializeObject(newTask), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/task/insert", content);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetAllTasks_ShouldWorkForAnyone()
    {
        var content = new StringContent("{}", Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/task/getall", content);
        response.EnsureSuccessStatusCode();
    }

    private async Task<string> GetTokenAsync(string username, string password)
    {
        var login = new { UserName = username, Password = password };
        var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/auth/login", content);
        var responseBody = await response.Content.ReadAsStringAsync();
        return (string)JsonConvert.DeserializeObject<dynamic>(responseBody).token;
    }

}
