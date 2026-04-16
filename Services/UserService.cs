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

        public async Task<bool> CreateUserAsync(UserCreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/users", dto);

            return response.IsSuccessStatusCode;
        }
    }
}