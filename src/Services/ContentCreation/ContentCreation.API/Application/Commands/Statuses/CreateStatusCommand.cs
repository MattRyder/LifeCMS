using System.Runtime.Serialization;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Statuses
{
    public class CreateStatusCommand : IRequest<BasicResponse>
    {
        [DataMember]
        public string Mood { get; private set; }

        [DataMember]
        public string Text { get; private set; }

        public CreateStatusCommand(string mood, string text)
        {
            Mood = mood;
            Text = text;
        }
    }
}