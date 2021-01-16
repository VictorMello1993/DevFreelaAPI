using DevFreela.Application.Queries.GetUser;
using DevFreela.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DevFreela.Application.Queries.SearchFreelancer
{
    public class SearchFreelancerQueryHandler : IRequestHandler<SearchFreelancerQuery, GetUserViewModel>
    {
        private readonly IUserRepository _userRepository;
        public SearchFreelancerQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserViewModel> Handle(SearchFreelancerQuery request, CancellationToken cancellationToken)
        {
            var freelancer = await _userRepository.GetUserFreelancerAsync(request.IdUser);

            if (freelancer == null)
            {
                return null;
            }

            var userViewModel = new GetUserViewModel(freelancer.Id, freelancer.Name, new List<UserSkillViewModel>());

            return userViewModel;
        }
    }
}
