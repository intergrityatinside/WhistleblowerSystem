﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Shared.Enums;
using System.Linq;

namespace WhistleblowerSystem.Client.Shared
{
    public partial class MainLayout
    {
        [Inject] IJSRuntime JSRuntime { get; set; } = null!;
        [Inject] NavigationManager Navigation { get; set; } = null!;
        [Inject] IStringLocalizer<App> L { get; set; } = null!;

        private string _userName = "";
        private UserDto? _currentUser;
        private WhistleblowerDto? _currentWhistleblower;
        List<(int, string)> languages = new() { 
            ((int)Language.German , "Deutsch"), 
            ((int)Language.English, "English")
        };

        private Language CurrentLanguage
        {
            get => LanguageService.Language;
            set
            {
                LanguageService.Language = value;
            }
        }

        async Task OnLanguageChanged(HashSet<Language> languages)
        {
            var language = languages.First();
            string cultureCode = language switch
            {
                Language.English => "en-US",
                Language.German => "de-DE",
                _ => throw new NotImplementedException()
            };

            await JSRuntime.InvokeVoidAsync("app.setToLocalStorage", "culture_code", cultureCode);
            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ICurrentAccountService CurrentAccountService { get; set; } = null!;


        public void DisposeAsync()
        {

            CurrentAccountService.CurrentUserChanged -= AccountService_CurrentUserChanged; 
            CurrentAccountService.CurrentWhistleblowerChanged -= AccountService_CurrentWhistleblowerChanged;
        }

        protected override void OnInitialized()
        {
            CurrentAccountService.CurrentUserChanged += AccountService_CurrentUserChanged;
            CurrentAccountService.CurrentWhistleblowerChanged += AccountService_CurrentWhistleblowerChanged;
            _currentUser = CurrentAccountService.GetCurrentUser();
            if (_currentUser != null) { SetUsername(_currentUser); }
            _currentWhistleblower = CurrentAccountService.GetCurrentWhistleblower();
            StateHasChanged();
        }

        private void AccountService_CurrentUserChanged(object? sender, CurrentUserChangedEventArgs e)
        {
            _currentUser = CurrentAccountService.GetCurrentUser();
            SetUsername(_currentUser);
            StateHasChanged();
        }

        private void AccountService_CurrentWhistleblowerChanged(object? sender, CurrentWhistleblowerChangedEventArgs e)
        {
            _currentWhistleblower = CurrentAccountService.GetCurrentWhistleblower();
            StateHasChanged();
        }

        private void SetUsername(UserDto? userDto)
        {
            _userName = userDto != null ? userDto.FirstName + " " + userDto.Name : "";
        }

        private string GetLanguageIconUrl(Language language) {
            return language == Language.German ? "https://upload.wikimedia.org/wikipedia/commons/b/ba/Flag_of_Germany.svg" : "https://upload.wikimedia.org/wikipedia/commons/a/ae/Flag_of_the_United_Kingdom.svg";
        }

    }
}
