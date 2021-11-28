using System;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;

namespace WhistleblowerSystem.Client.Services {
    public interface ICurrentAccountService
    {
        event EventHandler<CurrentUserChangedEventArgs>? CurrentUserChanged;
        public Task <UserDto?> Login(UserDto loginDto);
        public Task Logout();
        public UserDto? GetCurrentUser();
    }
}
