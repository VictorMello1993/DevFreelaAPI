//using DevFreela.Application.Queries.GetUser;
//using DevFreela.Domain.Interfaces.Repositories;
//using MediatR;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;

//namespace DevFreela.Application.Queries.SearchClient
//{
//    public class SearchClientQueryHandler : IRequestHandler<SearchClientQuery, GetUserViewModel>
//    {
//        private readonly IUserRepository _userRepository;

//        public SearchClientQueryHandler(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public async Task<GetUserViewModel> Handle(SearchClientQuery request, CancellationToken cancellationToken)
//        {
//            var client = await _userRepository.GetUserClientAsync(request.IdUser);

//            if(client == null)
//            {
//                return null;
//            }

//            var userViewModel = new GetUserViewModel(client.Id, client.Name, new List<UserSkillViewModel>());

//            return userViewModel;
//        }
//    }
//}
