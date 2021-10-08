using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Business.Utils;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Repositories;
using WhistleblowerSystem.Shared.Exceptions;

namespace WhistleblowerSystem.Business.Services
{
    public class UserService
    {
        readonly UserRepository _userRepository;
        readonly IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;

        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            if (userDto.Password == null) throw new NullException(nameof(userDto.Password));
            await _userRepository.CreateUser(userDto.CompanyId, PasswordUtils.HashPw(userDto.Password));
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> Authenticate(string id, string pw)
        {
            var user = await _userRepository.FindOneByIdAsync(id);
            if (user == null || user.PasswordHash == null || !PasswordUtils.Verify(pw, user.PasswordHash))
            {
                return false;
            }

            return true;
        }

        public async Task<UserDto?> FindOneByIdAsync(string id)
        {
            var user =  await _userRepository.FindOneByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }
    }
}
