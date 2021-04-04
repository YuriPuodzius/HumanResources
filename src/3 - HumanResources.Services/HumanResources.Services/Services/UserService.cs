using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HumanResources.Core.Exceptions;
using HumanResources.Domain.Entities;
using HumanResources.Infra.Interfaces;
using HumanResources.Services.DTO;
using HumanResources.Services.Interfaces;

namespace HumanResources.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Create(UserDTO userDto)
        {

            List<User> userExists = await _userRepository.SearchByEmail(userDto.Email);

            if (userExists.Any())
                throw new DomainException("Já existe um usuário cadastrado com o email informado!");

            User user = _mapper.Map<User>(userDto);
            user.Validate();

            User userCreated = await _userRepository.Create(user);

            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task<UserDTO> Update(UserDTO userDto)
        {
            var userExists = await _userRepository.Get(userDto.Id);

            if (userExists is null)
                throw new DomainException("Não existe nenhum usuário com o id informado!");

            User user = _mapper.Map<User>(userDto);
            user.Validate();

            var userCreated = await _userRepository.Update(user);

            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task Remove(long id)
        {
            var userExists = await _userRepository.Get(id);

            if (userExists is null)
                throw new DomainException("Não existe nenhum usuário com o id informado.");

            await _userRepository.Remove(id);
        }

        public async Task<UserDTO> Get(long id)
        {
            var userExists = await _userRepository.Get(id);

            if (userExists is null)
                throw new DomainException("Não existe nenhum usuário com o id informado.");

            return _mapper.Map<UserDTO>(userExists);
        }

        public async Task<List<UserDTO>> Get()
        {
            List<User> allUsers = await _userRepository.Get();

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByName(string name)
        {
            List<User> usersFound = await _userRepository.SearchByName(name);

            if (usersFound is null)
                throw new DomainException("Não foi encontrado nenhum resultado para o nome informado.");

            return _mapper.Map<List<UserDTO>>(usersFound);
        }

        public async Task<List<UserDTO>> SearchByEmail(string email)
        {
            List<User> usersFound = await _userRepository.SearchByEmail(email);

            if (usersFound is null)
                throw new DomainException("Não foi encontrado nenhum resultado para o email informado.");

            return _mapper.Map<List<UserDTO>>(usersFound);
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            User userExists = await _userRepository.GetByEmail(email);

            if (userExists is null)
                throw new DomainException("Não existe nenhum usuário com o email informado.");

            return _mapper.Map<UserDTO>(userExists);
        }
    }
}
