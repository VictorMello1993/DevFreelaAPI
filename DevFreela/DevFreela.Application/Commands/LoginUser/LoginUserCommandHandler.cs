using Dapper;
using DevFreela.Domain.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public LoginUserCommandHandler(IConfiguration configuration, DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _connectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var encryptedPassword = LoginService.ComputeSha256Hash(request.Password);

            //Entity Framework
            //var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == encryptedPassword);

            //if (user == null)
            //{
            //    return null;
            //}

            //return new LoginUserViewModel(user.Email, GenerateJwtToken(user.Email, user.Role));

            //Dapper
            using(var sqlConnection = new MySqlConnection(_connectionString))
            {                
                var sql = @"SELECT Email, Password, Role FROM Users
                            WHERE Email = @email
                            AND Password = @password";

                var result = await sqlConnection.QueryAsync<User>(sql, new { email = request.Email, password = encryptedPassword });
                var user = result.FirstOrDefault();

                if(user == null)
                {
                    return null;
                }

                return new LoginUserViewModel(user.Email, GenerateJwtToken(user.Email, user.Role));
            }
        }

        private string GenerateJwtToken(string email, string role)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("userName", email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: DateTime.Now.AddDays(7), 
                                             signingCredentials: credentials, claims: claims);

            var tokenHandler = new JwtSecurityTokenHandler();

            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
