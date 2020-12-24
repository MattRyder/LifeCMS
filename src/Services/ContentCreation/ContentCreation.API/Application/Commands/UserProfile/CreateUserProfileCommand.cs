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

        public Uri AvatarImageUri { get; private set; }

        public Uri HeaderImageUri { get; private set; }

        public CreateUserProfileCommand(
            string name,
            string email_address,
            string occupation,
            string location,
            string bio,
            Uri avatar_image_uri,
            Uri header_image_uri
        )
        {
            Name = name;

            EmailAddress = new EmailAddress(email_address);

            Occupation = occupation;

            Location = location;

            Bio = bio;

            AvatarImageUri = avatar_image_uri;

            HeaderImageUri = header_image_uri;
        }
    }
}
