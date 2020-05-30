using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Posts
{
    public class CreatePostCommand : IRequest<bool>
    {
        [Required]
        public Guid AuthorId { get; private set; }

        [Required]
        public string Title { get; private set; }

        [Required]
        public string Text { get; private set; }

        public CreatePostCommand(Guid authorId, string title, string text)
        {
            AuthorId = authorId;
            Title = title;
            Text = text;
        }
    }
}