using System;
using WhistleblowerSystem.Shared.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public class CurrentUserChangedEventArgs : EventArgs
    {
        public UserDto? CurrentUser { get; }

        public CurrentUserChangedEventArgs(UserDto? currentUser)
        {
            CurrentUser = currentUser;
        }
    }
}
