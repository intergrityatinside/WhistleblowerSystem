using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Shared.Exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(string msg, Language language) : base(msg)
        {
            Language = language;
        }

        public Language Language { get; }
    }
}
