using DevFreela.Application.Commands.CreateUser;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;
using System;
using DevFreela.Domain.Enums;
using System.Threading;


namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTests 
    {
        [Fact]
        public async Task DataIsValid_Executed_ReturnValidViewModel()
        {
            //Arrange (organizar classes de teste)
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(ur => ur.Add(It.IsAny<User>())).Verifiable(); //Verificar se a entidade User foi chamado no repository

            var createUserCommand = new CreateUserCommand("Zezinho Oliveira", "zezinho.oliveira@email.com", new DateTime(1995, 1, 1), "123456", "Client");
            var createUserCommandHandler = new CreateUserCommandHandler(userRepository.Object);

            //Act (operação a ser testada)
            var result = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());

            //Assert (verificação dos resultados)
            Assert.NotNull(result);
            Assert.Equal(createUserCommand.Name, result.Name);
            Assert.Equal(createUserCommand.Email, result.Email);
            Assert.Equal(createUserCommand.BirthDate, result.BirthDate);
            //Assert.Equal(createUserCommand.UserType, result.UserType);

            userRepository.Verify(ur => ur.Add(It.IsAny<User>()), Times.Once);
        }
    }
}
