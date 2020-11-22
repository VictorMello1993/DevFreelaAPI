using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserViewModel>
    {        
        //private readonly DevFreelaDbContext _dbContext;
        //public GetUserQueryHandler(DevFreelaDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}
        
        //Usando o padrão repository
        private readonly IUserRepository _userRepository;
        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            //var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.IdUser);
            var user = await _userRepository.GetUserAsync(request.IdUser);

            if (user == null)
            {
                return null;
            }

            var userViewModel = new GetUserViewModel(user.Id, user.Name, new List<UserSkillViewModel>());

            return userViewModel;
        }
    }
}
