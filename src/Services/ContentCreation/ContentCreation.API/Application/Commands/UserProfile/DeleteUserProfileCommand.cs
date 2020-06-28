using System;
using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles
{
    public class DeleteUserProfileCommand : IRequest<bool>
    {
        [Required]
        public Guid Id { get; private set; }

        public DeleteUserProfileCommand(Guid id)
        {
            Id = id;
        }
    }
}