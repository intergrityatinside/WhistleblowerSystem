using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public class CurrentAccountService : ICurrentAccountService
    {
        private readonly HttpClient _http;
        public event EventHandler<CurrentUserChangedEventArgs>? CurrentUserChanged;
        private UserDto? _currentUser;

        public CurrentAccountService(HttpClient http)
        {
            _http = http;
        }

        public async Task InitAsync()
        {
            var response = await _http.GetAsync("Authentication");
            if(!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
            {
                _currentUser = await response.Content.ReadFromJsonAsync<UserDto>();
            }
        }

        public async Task Login(UserDto userDto)
        {
            var response = await _http.PostAsJsonAsync("Authentication/login", userDto);
            _currentUser = !response.IsSuccessStatusCode ? null : await response.Content.ReadFromJsonAsync<UserDto?>();
            CurrentUserChanged?.Invoke(this, new CurrentUserChangedEventArgs(_currentUser));
        }

        public async Task Logout()
        {
            await _http.PostAsJsonAsync("Authentication/logout", _currentUser);
            _currentUser = null;
            CurrentUserChanged?.Invoke(this, new CurrentUserChangedEventArgs(null));
        }

        public UserDto? GetCurrentUser()
        {
            return _currentUser;
        }
    }
}
