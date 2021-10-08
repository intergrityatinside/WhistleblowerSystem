using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Shared.Exceptions
{
    public class NullException : BaseException
    {

        public NullException(string parentName, string name) : base($"The following is null: {parentName}.{name}", Language.English)
        {
        }

        public NullException(string parentName, string name, string methodName) : base($"The following is null: {parentName}.{name} in {methodName}", Language.English)
        {

        }

        public NullException(string name) : base($"The following is null: {name}", Language.English)
        {

        }



    }
}
