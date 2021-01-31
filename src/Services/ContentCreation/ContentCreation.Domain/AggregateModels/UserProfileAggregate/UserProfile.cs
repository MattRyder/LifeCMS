using System;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate
{
    public class UserProfile : BaseEntity, IAggregateRoot
    {
        public Guid UserId { get; private set; }

        public string Name { get; private set; }

        public EmailAddress EmailAddress { get; private set; }

        public string Occupation { get; private set; }

        public string Location { get; private set; }

        public string Bio { get; private set; }

        public string AvatarImageUrn { get; private set; }

        public string HeaderImageUrn { get; private set; }

        // A constructor without a value object argument (EmailAddress) is required for EF Core right now
        // https://github.com/dotnet/efcore/issues/12078
        private UserProfile(Guid userId, string name)
        {
            UserId = userId != null ? userId : throw new UserProfileDomainException(nameof(userId));

            Name = name ?? throw new UserProfileDomainException(nameof(name));
        }

        public UserProfile(
            Guid userId,
            string name,
            EmailAddress emailAddress,
            string occupation,
            string location,
            string bio,
            string avatarImageUrn,
            string headerImageUrn
            )
        : this(userId, name)
        {
            EmailAddress = emailAddress;

            Occupation = occupation;

            Location = location;

            Bio = bio;

            AvatarImageUrn = avatarImageUrn;

            HeaderImageUrn = headerImageUrn;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new UserProfileDomainException(nameof(name));
            }

            Name = name;
        }

        public void UpdateEmailAddress(EmailAddress emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public void UpdateOccupation(string occupation)
        {
            Occupation = occupation;
        }

        public void UpdateLocation(string location)
        {
            Location = location;
        }

        public void UpdateBio(string bio)
        {
            Bio = bio;
        }

        public void UpdateAvatarUrn(string avatarImageUrn)
        {
            AvatarImageUrn = avatarImageUrn;
        }

        public void UpdateHeaderImageUrn(string headerImageUrn)
        {
            HeaderImageUrn = headerImageUrn;
        }

    }
}
