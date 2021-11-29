using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public class CurrentAccountService : ICurrentAccountService
    {
        private readonly HttpClient _http;
        public event EventHandler<CurrentUserChangedEventArgs>? CurrentUserChanged;
        public event EventHandler<CurrentWhistleblowerChangedEventArgs>? CurrentWhistleblowerChanged;
        private UserDto? _currentUser;
        private WhistleblowerDto? _currentWhistleblower;

        public CurrentAccountService(HttpClient http)
        {
            _http = http;
        }

        public async Task InitAsync()
        {
            var responseUser = await _http.GetAsync("Authentication/user");
            if(!string.IsNullOrEmpty(await responseUser.Content.ReadAsStringAsync()))
            {
                _currentUser = await responseUser.Content.ReadFromJsonAsync<UserDto>();
            }

            var responseWhistleblower = await _http.GetAsync("Authentication/whistleblower");
            if (!string.IsNullOrEmpty(await responseWhistleblower.Content.ReadAsStringAsync()))
            {
                _currentWhistleblower = await responseWhistleblower.Content.ReadFromJsonAsync<WhistleblowerDto>();
            }
        }

        public async Task<UserDto?> Login(UserDto userDto)
        {
            var response = await _http.PostAsJsonAsync("Authentication/user/login", userDto);
            _currentUser = !response.IsSuccessStatusCode ? null : await response.Content.ReadFromJsonAsync<UserDto?>();
            CurrentUserChanged?.Invoke(this, new CurrentUserChangedEventArgs(_currentUser));
            return _currentUser;
        }

        public async Task<WhistleblowerDto?> Login(WhistleblowerDto whistleblowerDto)
        {
            var response = await _http.PostAsJsonAsync("Authentication/whistleblower/login", whistleblowerDto);
            _currentWhistleblower = !response.IsSuccessStatusCode ? null : await response.Content.ReadFromJsonAsync<WhistleblowerDto?>();
            CurrentWhistleblowerChanged?.Invoke(this, new CurrentWhistleblowerChangedEventArgs(_currentWhistleblower));
            return _currentWhistleblower;
        }

        public async Task Logout()
        {
            if (_currentUser != null) {
                await _http.PostAsJsonAsync("Authentication/logout", _currentUser);
                _currentUser = null;
                CurrentUserChanged?.Invoke(this, new CurrentUserChangedEventArgs(null));
                _currentWhistleblower = null;
                CurrentWhistleblowerChanged?.Invoke(this, new CurrentWhistleblowerChangedEventArgs(null));
            }
        }

        public UserDto? GetCurrentUser()
        {
            return _currentUser;
        }

        public WhistleblowerDto? GetCurrentWhistleblower()
        {
            return _currentWhistleblower;
        }
    }
}
