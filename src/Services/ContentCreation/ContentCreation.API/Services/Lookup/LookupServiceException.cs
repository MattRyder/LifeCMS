using System;

namespace LifeCMS.Services.ContentCreation.API.Services.Lookup
{
    public class LookupServiceException : Exception
    {
        public LookupServiceException(string message) : base(message) { }
    }
}
