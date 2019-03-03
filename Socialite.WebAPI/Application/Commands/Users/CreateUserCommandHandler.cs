using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Socialite.Domain.AggregateModels.UsersAggregate;

namespace Socialite.WebAPI.Application.Commands.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Email, request.Name);

            _userRepository.Add(user);

            return _userRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}