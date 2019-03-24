using System;

namespace Socialite.Domain.AggregateModels.AlbumAggregate
{
    public class AlbumDomainException : Exception
    {
        public AlbumDomainException(string message) : base(message)
        {
        }
    }
}