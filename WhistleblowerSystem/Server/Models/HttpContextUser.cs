using System;

namespace WhistleblowerSystem.Server.Models
{
    public class HttpContextUser
    {
        public HttpContextUser(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
