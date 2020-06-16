using System;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate
{
    public class UserProfile : BaseEntity, IAggregateRoot
    {
        public Guid UserId { get; private set; }

        public string Name { get; private set; }

        public EmailAddress EmailAddress { get; private set; }

        // A constructor without a value object argument (EmailAddress) is required for EF Core right now
        // https://github.com/dotnet/efcore/issues/12078
        private UserProfile(Guid userId, string name)
        {
            UserId = userId != null ? userId : throw new UserProfileDomainException(nameof(userId));

            Name = name ?? throw new UserProfileDomainException(nameof(name));
        }

        public UserProfile(Guid userId, string name, EmailAddress emailAddress)
        : this(userId, name)
        {
            EmailAddress = emailAddress ?? throw new UserProfileDomainException(nameof(emailAddress));
        }
    }
}