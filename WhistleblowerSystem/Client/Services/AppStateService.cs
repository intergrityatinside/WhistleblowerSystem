using System;
using WhistleblowerSystem.Business.DTOs;

public class AppStateService
{
    public FormDto? CurrentForm { get; private set; }

    public event Action? OnChange;

    public void SetForm(FormDto? form)
    {
        CurrentForm = form?? null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}