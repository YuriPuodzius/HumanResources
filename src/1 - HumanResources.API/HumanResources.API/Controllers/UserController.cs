using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HumanResources.API.Helpers;
using HumanResources.API.ViewModels;
using HumanResources.Core.Exceptions;
using HumanResources.Services.DTO;
using HumanResources.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        [Route("/api/v1/users/create")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel createUserViewModel)
        {
            try
            {
                UserDTO userDTO = _mapper.Map<UserDTO>(createUserViewModel);
                UserDTO userCreated = await _userService.Create(userDTO);

                return Ok(new ResultViewModel { Message = "Usuário criado com sucesso.", Success = true, Data = userCreated });
            }
            catch (DomainException e)
            {
                return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception e)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpPut]
        [Authorize]
        [Route("/api/v1/users/update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserViewModel updateUserViewModel)
        {
            try
            {
                UserDTO userDTO =  _mapper.Map<UserDTO>(updateUserViewModel);
                UserDTO userCreated = await _userService.Update(userDTO);

                return Ok(new ResultViewModel { Message = "Usuário atualizado com sucesso.", Success = true, Data = userCreated });
            }
            catch (DomainException e)
            {
                return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception e)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("/api/v1/users/delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {

                await _userService.Remove(id);

                return Ok(new ResultViewModel
                { Message = "Usuário removido com sucesso.", Success = true });
            }
            catch (DomainException e)
            {
                return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception e)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                UserDTO userDto = await _userService.Get(id);

                string messageResponse = "Usuário encontrado com sucesso.";

                if (userDto is null)
                    messageResponse = "Nenhum usuário foi encontrado com o ID informado.";

                return Ok(new ResultViewModel
                    { Message = messageResponse, Success = true, Data = userDto });
            }
            catch (DomainException e)
            {
                return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception e)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get-all")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<UserDTO> usersDto = await _userService.Get();

                return Ok(new ResultViewModel
                    { Message = $@"Usuários encontrados: ({usersDto.Count})", Success = true, Data = usersDto });
            }
            catch (DomainException e)
            {
                return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception e)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }


        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get-by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            try
            {
                UserDTO userDto = await _userService.GetByEmail(email);

                string messageResponse = "Usuário encontrado com sucesso.";

                if (userDto is null)
                    messageResponse = "Nenhum usuário foi encontrado com o email informado.";

                return Ok(new ResultViewModel
                    { Message = messageResponse, Success = true, Data = userDto });
            }
            catch (DomainException e)
            {
                return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception e)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/search-by-name")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            try
            {
                List<UserDTO> usersDto = await _userService.SearchByName(name);

                return Ok(new ResultViewModel
                    { Message = $@"Usuários encontrados: ({usersDto.Count})", Success = true, Data = usersDto });
            }
            catch (DomainException e)
            {
                return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception e)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/search-by-email")]
        public async Task<IActionResult> SearchByEmail([FromQuery] string email)
        {
            try
            {
                List<UserDTO> usersDto = await _userService.SearchByEmail(email);

                return Ok(new ResultViewModel
                    { Message = $@"Usuários encontrados: ({usersDto.Count})", Success = true, Data = usersDto });
            }
            catch (DomainException e)
            {
                return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception e)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
    }
}
