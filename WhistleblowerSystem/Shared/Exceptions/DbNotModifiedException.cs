using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Shared.Exceptions
{
    public class DbNotModifiedException : BaseException
    {
        public enum MethodType
        {
            Undefined = 0,
            Update = 1,
            Delete = 2,
            Replace = 3,
            InsertTransaction = 4
        }
        public DbNotModifiedException(long count, string message) : base(message, Language.English)
        {
            ModifiedCount = count;
        }

        public DbNotModifiedException(long count, MethodType method) : base($"DbNotModifiedException: Count: {count}, Method: {method}", Language.English)
        {
            ModifiedCount = count;
            Method = method;
        }

        public DbNotModifiedException(long count, MethodType method, string field) : base($"DbNotModifiedException: Count: {count}, Method: {method}, Field: {field}",
            Language.English)
        {
            ModifiedCount = count;
            Method = method;
            Field = field;
        }

        public DbNotModifiedException(long count, MethodType method, string field, string message) : base(message, Language.English)
        {
            ModifiedCount = count;
            Method = method;
            Field = field;
        }

        public long ModifiedCount { get; set; }
        public MethodType Method { get; set; }
        public string? Field { get; set; }
    }
}
