using System.Collections.Generic;

namespace LifeCMS.Services.Identity.Infrastructure.Responses
{
    public class BasicResponse
    {
        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public dynamic Data { get; set; }
    }
}