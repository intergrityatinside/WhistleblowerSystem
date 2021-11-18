using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs.Config;
using WhistleblowerSystem.Client.Services;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Client.Utils
{
    public static class LanguageHelper
    {
        public static LanguageEntryDto GetLng(this List<LanguageEntryDto> languageEntries)
        {
           return languageEntries.FirstOrDefault(x => x.Language == LanguageService.Language) ??
                languageEntries.First(x => x.Language == Language.German);
        }
    }
}
