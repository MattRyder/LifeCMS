using System;
using Newtonsoft.Json;

namespace LifeCMS.EventBus.Common.Events
{
    public class IntegrationEvent
    {
        [JsonProperty]
        public Guid Id { get; private set; }

        [JsonProperty]
        public DateTime CreatedAt { get; private set; }

        public IntegrationEvent()
        {
            Id = new Guid();

            CreatedAt = DateTime.UtcNow;
        }

        public IntegrationEvent(Guid id, DateTime createdAt)
        {
            Id = id;

            CreatedAt = createdAt;
        }
    }
}