using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;
using Socialite.Domain.AggregateModels.StatusAggregate;

namespace Socialite.WebAPI.Application.Commands.Statuses
{
    public class CreateStatusCommand : IRequest<bool>
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