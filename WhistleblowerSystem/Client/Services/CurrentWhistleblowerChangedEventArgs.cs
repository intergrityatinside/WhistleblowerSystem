using System;
using WhistleblowerSystem.Shared.DTOs;

namespace WhistleblowerSystem.Client.Services
{
    public class CurrentWhistleblowerChangedEventArgs : EventArgs
    {
        public WhistleblowerDto? CurrentWhistleblower { get; }

        public CurrentWhistleblowerChangedEventArgs(WhistleblowerDto? currentWhistleblower)
        {
            CurrentWhistleblower = currentWhistleblower;
        }
    }
}
