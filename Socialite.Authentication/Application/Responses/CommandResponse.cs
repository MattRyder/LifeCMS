using System.Collections.Generic;

namespace Socialite.Authentication.Application.Responses
{
    public class CommandResponse
    {
        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public dynamic Data { get; set;}
    }
}