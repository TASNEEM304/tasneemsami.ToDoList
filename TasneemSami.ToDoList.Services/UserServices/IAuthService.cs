using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasneemSami.ToDoList.Database.GeniricRepository;
using TasneemSami.ToDoList.Database.Models;

namespace TasneemSami.ToDoList.Services.UserServices
{
    public interface IAuthService: IRepository<Users>
    {
        Task RegisterAsync(UserInsertAndUpdateDto input);
        Task<UserGetOutPutDto?> ValidateUserAsync(LoginRequest input);
      

    }
}
