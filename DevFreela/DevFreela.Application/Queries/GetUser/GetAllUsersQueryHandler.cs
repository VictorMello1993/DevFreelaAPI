using DevFreela.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetUserViewModel>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<GetUserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();
            var usersViewModel = new List<GetUserViewModel>();

            if(users == null)
            {
                return null;
            }

            users.ForEach(u => usersViewModel.Add(new GetUserViewModel(u.Id, u.Name, new List<UserSkillViewModel>())));

            return usersViewModel;
        }
    }
}
