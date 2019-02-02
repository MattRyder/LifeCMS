using Socialite.Domain.Common;

public interface IEventHandle<T> where T : BaseEvent
{
  void Handle(T domainEvent);
}