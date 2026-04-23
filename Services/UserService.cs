using DocArchiveAdmin.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocArchiveAdmin.Services
{
    public class UserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        // =========================
        // CREATE USER
        // =========================
        public async Task<bool> CreateUserAsync(UserCreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/users", dto);
            return response.IsSuccessStatusCode;
        }

        // =========================
        // GET ALL USERS (FOR TABLE)
        // =========================
        public async Task<List<UserListDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _http.GetFromJsonAsync<List<UserListDto>>("api/users");

                return users ?? new List<UserListDto>();
            }
            catch
            {
                return new List<UserListDto>();
            }
        }

        // =========================
        // GET ROLES
        // =========================
        public async Task<List<RoleDto>> GetRolesAsync()
        {
            try
            {
                var roles = await _http.GetFromJsonAsync<List<RoleDto>>("api/roles");
                return roles ?? new List<RoleDto>();
            }
            catch
            {
                return new List<RoleDto>();
            }
        }

        // =========================
        // RESET PASSWORD
        // =========================
        public async Task<bool> ResetPasswordAsync(int userId)
        {
            try
            {
                var response = await _http.PostAsync($"api/users/{userId}/reset-password", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // =========================
        // ACTIVATE / DEACTIVATE USER
        // =========================
        public async Task<bool> ToggleUserStatusAsync(int userId)
        {
            try
            {
                var response = await _http.PutAsync($"api/users/{userId}/toggle-status", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateProfile(string contactNumber, string address)
        {
            try
            {
                var token = await SecureStorage.GetAsync("jwt_token");

                if (string.IsNullOrWhiteSpace(token))
                    throw new Exception("JWT token not found.");

                // IMPORTANT: avoid duplicate headers bug
                _http.DefaultRequestHeaders.Authorization = null;
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var data = new
                {
                    ContactNumber = contactNumber,
                    Address = address
                };

                var json = JsonSerializer.Serialize(data);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PutAsync("api/users/update-profile", content);

                Console.WriteLine($"STATUS: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API ERROR: {error}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SERVICE ERROR: {ex.Message}");
                return false;
            }
        }

        // =========================
        // GET SINGLE USER (OPTIONAL - VIEW PAGE)
        // =========================
        public async Task<UserListDto?> GetUserByIdAsync(int userId)
        {
            try
            {
                return await _http.GetFromJsonAsync<UserListDto>($"api/users/{userId}");
            }
            catch
            {
                return null;
            }
        }
    }
}