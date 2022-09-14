using API__NET_5.Models.Dto;
using API__NET_5.Models;
using API__NET_5.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API__NET_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepositorio _userRepositorio;
        protected response _response;

        public UsersController(IUserRepositorio userRepositorio)
        {
            _userRepositorio = userRepositorio;
            _response = new response();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(DtoUser user)
        {
            var respuesta = await _userRepositorio.Register(
                new user
                {
                    userName = user.userName
                }, user.password);
            if(respuesta == -1)
            {
                _response.IsSuccess = false;
                _response.Message = "Usuario ya existe";
                return BadRequest(_response);
            }

            if(respuesta == -500)
            {
                _response.IsSuccess = false;
                _response.Message = "Error al crear el usuario";
                return BadRequest(_response);
            }

            _response.Message = "Usuario created";
            _response.Result = respuesta;
            return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(DtoUser user)
        {
            var respuesta = await _userRepositorio.Login(user.userName, user.password);

            if(respuesta == "nouser")
            {
                _response.IsSuccess = false;
                _response.Message = "Usuario no existe";
                return BadRequest(_response);
            }
            if(respuesta == "wrongpassword")
            {
                _response.IsSuccess = false;
                _response.Message = "Contraseña Incorrecta";
                return BadRequest(_response);
            }

            _response.Result = respuesta;
            _response.Message = "Usuario conectado";
            return Ok(_response);
        }
    }
}
