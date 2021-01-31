using System;
using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles
{
    public class DeleteUserProfileCommand : IRequest<BasicResponse>
    {
        [Required]
        public Guid Id { get; private set; }

        public DeleteUserProfileCommand(Guid id)
        {
            Id = id;
        }
    }
}
