using DevFreela.Application.Queries.GetSkill;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using System.Linq;

namespace DevFreela.UnitTests.Application.Queries
{
    //Padrão Given When Then 
    public class GetAllSkillsQueryTests
    {
        [Fact]
        public async Task ExistThreeSkills_Executed_ThreeSkillsViewModel()
        {
            //Arrange
            var skills = new List<Skill>
            {
                new Skill(".NET"),
                new Skill("Angular"),
                new Skill("SQL Server")
            };

            var skillsRepository = new Mock<ISkillRepository>();
            skillsRepository.Setup(sr => sr.GetAllSkillsAsync()).Returns(Task.FromResult(skills)); //Mockando os dados da entidade para as classes do CRQS e validá-los posteriormente

            var getAllSkillsQuery = new GetAllSkillsQuery();
            var getAllSkillsQueryHandler = new GetAllSkillsQueryHandler(skillsRepository.Object);

            //Act
            var skillsViewModel = await getAllSkillsQueryHandler.Handle(getAllSkillsQuery, new CancellationToken());

            //Assert
            Assert.NotNull(skillsViewModel);
            Assert.Equal(skills.Count, skillsViewModel.Count);

            foreach (var item in skills)
            {
                var skillViewModel = skillsViewModel.FirstOrDefault(s => s.Description == item.Description);

                Assert.NotNull(skillViewModel);
            }

            skillsRepository.Verify(sr => sr.GetAllSkillsAsync(), Times.Once);

        }
    }
}
