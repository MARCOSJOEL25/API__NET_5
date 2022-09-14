using API__NET_5.Data;
using API__NET_5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API__NET_5.Repositorio
{
    public class UserRepositorio : IUserRepositorio
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public UserRepositorio(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<string> Login(string userName, string password)
        {
            var user = await _context.user.FirstOrDefaultAsync(x => x.userName.ToLower().Equals(userName.ToLower()));
            if(user == null)
            {
                return "nouser";
            } else if (!verificarPasswordHash(password, user.passwordHash, user.passwordSalt)){
                return "wrongpassword";
            } else
            {
                return crearToken(user);
            }
        }

        public async Task<int> Register(user user, string password)
        {
            try
            {
                if (await UserExiste(user.userName))
                {
                    return -1;
                }

                createPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.passwordHash = passwordHash;
                user.passwordSalt = passwordSalt;

                await _context.user.AddAsync(user);
                await _context.SaveChangesAsync();
                return user.id;
            }
            catch (Exception)
            {
                return -500;
            }
        }

        public async Task<bool> UserExiste(string username)
        {
            if (await _context.user.AnyAsync(x => x.userName.ToLower().Equals(username.ToLower())))
            {
                return true;
            }

            return false;
        }

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool verificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public string crearToken(user user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.userName)
            };

            var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
