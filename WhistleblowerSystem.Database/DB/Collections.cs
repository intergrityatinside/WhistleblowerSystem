using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities;

namespace WhistleblowerSystem.Database.DB
{
    public static class Collections
    {

        private const string Attachements = "Attachements";
        private const string Companies = "Companies";
        private const string Forms = "Forms";
        private const string FormFields = "FormFields";
        private const string FormTemplates = "FormTemplates";
        private const string FormFieldTemplates = "FormFieldTemplates";
        private const string Topics = "Topics";
        private const string Users = "Users";

        private static readonly IDictionary<Type, string> CollectionDictonary = new Dictionary<Type, string>()
        {
            { typeof(Attachement), Attachements },
            { typeof(Company), Companies },
            { typeof(Form), Forms },
            { typeof(FormField), FormFields },
            { typeof(FormTemplate), FormTemplates },
            { typeof(FormFieldTemplate), FormFieldTemplates },
            { typeof(Topic), Topics },
            { typeof(User), Users },
        };

        public static string GetCollectionName<T>()
        {
            return CollectionDictonary[typeof(T)];
        }

        public static IEnumerable<Type> GetTypes()
        {
            return CollectionDictonary.Keys;
        }

        public static IEnumerable<string> GetCollectionNames()
        {
            return CollectionDictonary.Values;
        }
    }
}
