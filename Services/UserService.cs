using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
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
        // GET ROLES (NEW)
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
    }
}