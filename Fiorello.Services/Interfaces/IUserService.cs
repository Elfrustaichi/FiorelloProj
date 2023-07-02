using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Interfaces
{
    public interface IUserService
    {
        public CreatedItemGetDto Create(UserCreateDto dto);

        public void Delete(string id);

        public List<UserGetAllItemsDto> GetAll();
    }
}
