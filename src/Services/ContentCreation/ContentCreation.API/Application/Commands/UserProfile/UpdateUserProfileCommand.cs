using System;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles
{
    public class UpdateUserProfileCommand : IRequest<BasicResponse>
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public EmailAddress EmailAddress { get; private set; }

        public string Occupation { get; private set; }

        public string Location { get; private set; }

        public string Bio { get; private set; }

        public string AvatarImageUrn { get; private set; }

        public string HeaderImageUrn { get; private set; }

        public UpdateUserProfileCommand(
            Guid id,
            string name,
            string email_address,
            string occupation,
            string location,
            string bio,
            string avatar_image_urn,
            string header_image_urn)
        {
            Id = id;

            Name = name;

            EmailAddress = !string.IsNullOrEmpty(email_address)
                ? new EmailAddress(email_address)
                : null;

            Occupation = occupation;

            Location = location;

            Bio = bio;

            AvatarImageUrn = avatar_image_urn;

            HeaderImageUrn = header_image_urn;
        }
    }
}
