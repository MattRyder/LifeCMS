using System;
using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Domain.Common;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles
{
    public class CreateUserProfileCommand : IRequest<bool>
    {
        [Required]
        public string Name { get; private set; }

        public EmailAddress EmailAddress { get; private set; }

        public string Occupation { get; private set; }

        public string Location { get; private set; }

        public string Bio { get; private set; }

        public string AvatarImageUrn { get; private set; }

        public string HeaderImageUrn { get; private set; }

        public CreateUserProfileCommand(
            string name,
            string email_address,
            string occupation,
            string location,
            string bio,
            string avatar_image_urn,
            string header_image_urn
        )
        {
            Name = name;

            EmailAddress = new EmailAddress(email_address);

            Occupation = occupation;

            Location = location;

            Bio = bio;

            AvatarImageUrn = avatar_image_urn;

            HeaderImageUrn = header_image_urn;
        }
    }
}
