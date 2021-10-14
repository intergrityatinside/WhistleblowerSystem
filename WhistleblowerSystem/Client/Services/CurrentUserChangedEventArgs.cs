using System;
using WhistleblowerSystem.Business.DTOs;

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
