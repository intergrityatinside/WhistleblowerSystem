using System;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;

namespace WhistleblowerSystem.Client.Services {
    public interface ICurrentAccountService
    {
        event EventHandler<CurrentUserChangedEventArgs>? CurrentUserChanged;
        public Task Login(UserDto loginDto);
        public Task Logout();
        public UserDto? GetCurrentUser();
    }
}
