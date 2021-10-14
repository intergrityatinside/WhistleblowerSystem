using System;

namespace WhistleblowerSystem.Server.Models
{
    public class HttpContextUser
    {
        public HttpContextUser(string id, string companyId)
        {
            Id = id;
            CompanyId = companyId;
        }

        public string Id { get; }
        public string CompanyId { get; }
    }
}
