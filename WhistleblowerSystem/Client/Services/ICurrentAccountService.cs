using System;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;

namespace WhistleblowerSystem.Client.Services {
    public interface ICurrentAccountService
    {
        event EventHandler<CurrentUserChangedEventArgs>? CurrentUserChanged;
        event EventHandler<CurrentWhistleblowerChangedEventArgs>? CurrentWhistleblowerChanged;
        public Task <UserDto?> Login(UserDto loginDto);
        public Task<WhistleblowerDto?> Login(WhistleblowerDto whistleblowerDto);
        public Task Logout();
        public UserDto? GetCurrentUser();
        public WhistleblowerDto? GetCurrentWhistleblower();
    }
}
