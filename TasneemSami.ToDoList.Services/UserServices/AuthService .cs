using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasneemSami.ToDoList.Database.Data;
using TasneemSami.ToDoList.Database.GeniricRepository;
using TasneemSami.ToDoList.Database.Models;

namespace TasneemSami.ToDoList.Services.UserServices
{
    public class AuthService :Repository<ToDoListDbContext, Users>, IAuthService
    {
        
       

        private IMapper _mapper;

        public AuthService(IMapper mapper, ToDoListDbContext context) : base(context)
        {
            _mapper = mapper;
            InitializeMapper();
        }
        public void InitializeMapper()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Users,LoginRequest>().ReverseMap();
                cfg.CreateMap<Users, UserInsertAndUpdateDto>().ReverseMap();
                cfg.CreateMap<Users, UserGetOutPutDto>().ReverseMap();
            }).CreateMapper();

        }
       

        public async Task RegisterAsync(UserInsertAndUpdateDto input)
        {
            var users = await base.GetAllAsync<Users>();
            if (users.Any(c=>c.UserName== input.UserName))
            {
                throw new Exception("Username already exists");
            }
            
              

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(input.Password);
            input.Password = hashedPassword;
            var res = _mapper.Map<Users>(input);
            await base.Insert<Users>(res);
           
        }

        public async Task<UserGetOutPutDto?> ValidateUserAsync(LoginRequest input)
        {
            var user = base.GetBy<Users>(c => c.UserName == input.UserName).FirstOrDefault();
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(input.Password, user.PASSWORD))
                return null;
            var res = _mapper.Map<UserGetOutPutDto>(user);
          
            return res;
        }

    }

}
