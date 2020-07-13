using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Newsletters
{
    public class DeleteNewsletterCommand : IRequest<bool>
    {
        [Required]
        public Guid Id { get; private set; }

        public DeleteNewsletterCommand(Guid id)
        {
            Id = id;
        }
    }
}