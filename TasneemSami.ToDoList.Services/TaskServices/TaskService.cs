using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TasneemSami.ToDoList.Database.Data;
using TasneemSami.ToDoList.Database.GeniricRepository;
using TasneemSami.ToDoList.Database.Models;
using TasneemSami.ToDoList.Services.UserServices;

namespace TasneemSami.ToDoList.Services.TaskServices
{
    public class TaskService : Repository<ToDoListDbContext, Tasks>, ITaskService
    {
        private  IMapper _mapper;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public TaskService(IMapper mapper,ToDoListDbContext context, IHttpContextAccessor httpContextAccessor) :base(context) 
        {
            _mapper = mapper;
            _HttpContextAccessor = httpContextAccessor;
            InitializeMapper();
        }
        public void InitializeMapper() {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tasks, TaskGetOutPutDto>().ReverseMap();
                cfg.CreateMap<Tasks, TaskInsertAndUpdateDto>().ReverseMap();
                cfg.CreateMap<Tasks, TaskGetOutPutDto>().ForMember(dest => dest.User, map => map.MapFrom(source => source.User != null ? new UserGetOutPutDto
                {
                    ID = source.User.ID,
                    UserName = source.User.UserName,

                } : null));
            } ).CreateMapper();

      }



        public GeneralOutput<TaskGetOutPutDto> GetAll(TaskSearchInput input)
        {
            IQueryable<TaskGetOutPutDto> res = _mapper.ProjectTo<TaskGetOutPutDto>(base.GetBy<Tasks>(c =>
            (input.UserId != null ? c.USERID == input.UserId : 1 == 1) &&
           (input.IsComplete != null ? c.ISCOMPLETED == input.IsComplete : 1 == 1) &&
            (input.Title != null ? c.TITLE.ToLower().Contains(input.Title) : 1 == 1)&&
            (input.search!=null?c.TITLE.ToLower().Contains(input.search):1==1)
            ));
            if (string.IsNullOrEmpty(input.OrderBy))
            {
                res = res.OrderBy(t => t.Priority); 
            }
            var result = PagedList<TaskGetOutPutDto>.ToPagedList(res.AsQueryable(),input);
            if (result.Items.Count() > 0)
            {
                result.Items = result.Items.AsQueryable();
            }
            return result;
        }
        public async Task<TaskGetOutPutDto?> GetById(int id)
        {
            var res = await base.GetBy<Tasks>(t => t.ID == id)
               
                .ProjectTo<TaskGetOutPutDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return res;
        }
        
        public async Task<TaskInsertAndUpdateDto> Insert(TaskInsertAndUpdateDto Object)
        {
            var res = _mapper.Map<Tasks>(Object);
            await base.Insert<Tasks>(res);
            return Object;
        }

        public async Task<string> Edit(TaskInsertAndUpdateDto Object)
        {
            var entity = base.GetBy<Tasks>(c => c.ID == Object.Id).FirstOrDefault();
            var res = _mapper.Map(Object, entity);
            await base.Update<Tasks>(res, res.ID);
            return "true";
        }


        public async Task<string> Delete(int id)
        {
            var res = base.Delete<Tasks>(await base.GetAsync<Tasks>(id));
            return await res == true ? "true" : "false"; 
        }

        public async Task<string> CompleteTask(int id)
        {
            
            var userIdClaim = _HttpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == "Id")?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("Invalid user ID.");

           
            var entity = base.GetBy<Tasks>(c => c.ID == id).FirstOrDefault();

            if (entity == null)
                throw new Exception("Task not found");

           
            if (entity.USERID != userId)
                throw new UnauthorizedAccessException("You are not allowed to complete this task.");

            
            entity.ISCOMPLETED = true;
            var res = await base.Update<Tasks>(entity, entity.ID);

            return "true";
        }

       

       
       

        
    }
}
