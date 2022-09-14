using API__NET_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API__NET_5.Repositorio
{
    public interface IUserRepositorio
    {
        Task<int> Register(user user, string password);
        Task<string> Login(string userName, string password);

        Task<bool> UserExiste(string username);
    }
}
