using System;
using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands
{
    public class CreateUserProfileCommand : IRequest<bool>
    {
        [Required]
        public Guid UserId { get; private set; }

        [Required]
        public string Name { get; private set; }

        public EmailAddress EmailAddress { get; private set; }

        public CreateUserProfileCommand(Guid userId, string name, EmailAddress emailAddress)
        {
            UserId = userId;

            Name = name;

            EmailAddress = emailAddress;
        }
    }
}