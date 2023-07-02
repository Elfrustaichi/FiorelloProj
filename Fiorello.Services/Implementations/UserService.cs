using AutoMapper;
using Fiorello.Core.Entities;
using Fiorello.Core.Repositories;
using Fiorello.Data.Repositories;
using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Dtos.User;
using Fiorello.Services.Exceptions;
using Fiorello.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public CreatedItemGetDto Create(UserCreateDto dto)
        {
            if (_userRepository.IsExist(x => x.UserName == dto.UserName)) throw new RestException(HttpStatusCode.BadRequest, "Username already exist");

            var user = _mapper.Map<AppUser>(dto);

            _userRepository.Add(user);
            _userRepository.Commit();

            return new CreatedItemGetDto { Id=313 };

        }

        public void Delete(string id)
        {
           var user= _userRepository.Get(x => x.Id == id);
            if (user==null) throw new RestException(HttpStatusCode.BadRequest, "User not found");

            _userRepository.Delete(user);
            _userRepository.Commit();
        }

        public List<UserGetAllItemsDto> GetAll()
        {
           var entities= _userRepository.Get(x=>true);

            var data = _mapper.Map<List<UserGetAllItemsDto>>(entities);

            return data;
        }
    }
}
