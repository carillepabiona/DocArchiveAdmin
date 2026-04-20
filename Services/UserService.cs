using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DocArchiveAdmin.DTOs;

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