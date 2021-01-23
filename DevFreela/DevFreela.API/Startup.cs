using DevFreela.API.Extensions;
using DevFreela.API.Filters;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Application.Validators;
using DevFreela.Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace DevFreela.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(o => o.Filters.Add(typeof(ValidationFilter))) //Configurando filtros de validação
                    .AddFluentValidation(o => o.RegisterValidatorsFromAssemblyContaining<CreateUserInputModelValidator>()); //Adicionando classes de validação para configurar Fluent Validation

            //Conexão com banco de dados com EF Core
            var connectionString = Configuration.GetConnectionString("DevFreelaMySQL");

            //Injeção de dependência dos repositories
            //services.AddScoped<IUserRepository, UserRepository>(); //Singleton de User para cada requisição

            /*Adicionando singletons para cada entidade. 
            Para cada requisição, será utilizada a mesma instância de cada entidade, através do método AddScoped()*/
            services.AddRepositories() 
                    .AddDbContext<DevFreelaDbContext>(options => options.UseMySql(connectionString)); //Banco de dados MySQL

            //Banco de dados em memória
            //services.AddRepositories()
            //        .AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase(connectionString));

            //Obtém todas as classes do assembly do projeto que implementam as interfaces IRequest e IRequestHandler => GetUserQuery
            services.AddMediatR(typeof(GetUserQuery))
                    .AddMemoryCache();

            //Configuração do Swagger
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela API", Version = "v1" }));

            //Configurações para gerar tokens de autenticação JWT do usuário
            services
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,

                      ValidIssuer = Configuration["Jwt:Issuer"],
                      ValidAudience = Configuration["Jwt:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                  };
              });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFreela API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
