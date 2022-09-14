using API__NET_5.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API__NET_5.Repositorio
{
    public interface IPlayerRepositorio
    {
        Task<List<DtoPlayer>> GetPlayer();

        Task<DtoPlayer> GetPlayerById(int id);

        Task<DtoPlayer> CreateUpdate(DtoPlayer DtoPlayer);

        Task<bool> DeletePlayer(int id);
    }
}