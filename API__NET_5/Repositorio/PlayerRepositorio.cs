using API__NET_5.Data;
using API__NET_5.Models;
using API__NET_5.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API__NET_5.Repositorio
{
    public class PlayerRepositorio : IPlayerRepositorio
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;

        public PlayerRepositorio(AppDbContext context, IMapper Mapper)
        {
            _context = context;
            _mapper = Mapper;
        }
        public async Task<DtoPlayer> CreateUpdate(DtoPlayer DtoPlayer)
        {
            player player = _mapper.Map<DtoPlayer, player>(DtoPlayer);
            if(player.id > 0)
            {
                _context.player.Update(player);
            }
            else
            {
                await _context.player.AddAsync(player);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<player, DtoPlayer>(player);
        }

        public async Task<bool> DeletePlayer(int id)
        {
            try
            {
                player player = await _context.player.FindAsync(id);
                if(player == null)
                {
                    return false;
                }
                _context.player.Remove(player);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<DtoPlayer>> GetPlayer()
        {
            List<player> list = await _context.player.ToListAsync();

            return _mapper.Map<List<DtoPlayer>>(list);
        }

        public async Task<DtoPlayer> GetPlayerById(int id)
        {
            player player = await _context.player.FindAsync(id);

            return _mapper.Map<DtoPlayer>(player);
        }
    }
}
