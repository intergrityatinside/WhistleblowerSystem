using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Server.Models
{
    public class HttpContextWhistleblower
    {
        public HttpContextWhistleblower(string id, string formId)
        {
            Id = id;
            FormId = formId;
        }

        public string Id { get; }
        public string FormId { get; }
    }
}
