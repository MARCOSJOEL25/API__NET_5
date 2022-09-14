using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API__NET_5.Data;
using API__NET_5.Models;
using API__NET_5.Repositorio;
using API__NET_5.Models.Dto;
using Microsoft.AspNetCore.Authorization;

namespace API__NET_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class playersController : ControllerBase
    {
        private readonly IPlayerRepositorio _repositorio;
        protected response _response;
        public playersController(IPlayerRepositorio IPlayerRepositorio)
        {
            _repositorio = IPlayerRepositorio;
            _response = new response();
        }

        // GET: api/players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<player>>> Getplayer()
        {
            try
            {
                var lista = await _repositorio.GetPlayer();
                _response.Result = lista;
                _response.Message = "Lista de Player";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Error = new List<string> { ex.ToString() };
            }
            return Ok(_response);

        }

        // GET: api/players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<player>> Getplayer(int id)
        {
            var player = await _repositorio.GetPlayerById(id);
            if (player == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Cliente no existe";
                return NotFound(_response);
            }
            _response.Result = player;
            _response.Message = "Informacion clinete";
            return Ok(_response);
        }

        // PUT: api/players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putplayer(int id, DtoPlayer DtoPlayer)
        {
            try
            {
                DtoPlayer model = await _repositorio.CreateUpdate(DtoPlayer);
                _response.Result = model;
                return Ok(_response); 
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Error al actualizar el registro";
                _response.Error = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<player>> Postplayer(DtoPlayer DtoPlayer)
        {
            try
            {
                DtoPlayer model = await _repositorio.CreateUpdate(DtoPlayer);
                _response.Result = model;
                return CreatedAtAction("Getplayer", new { id = model.id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Error al tratar de guardar un nuevo registro";
                _response.Error = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteplayer(int id)
        {
            try
            {
                bool IsDelete = await _repositorio.DeletePlayer(id);
                if (IsDelete)
                {
                    _response.Result = IsDelete;
                    _response.Message = "Player eliminado";
                    return Ok(_response);
                }
                else
                {
                    _response.Result = IsDelete;
                    _response.Message = "Error al eliminar player";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Error = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

       /* private bool playerExists(int id)
        {
        }*/
    }
}
