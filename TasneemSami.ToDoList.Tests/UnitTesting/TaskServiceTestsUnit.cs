using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TasneemSami.ToDoList.Database.Data;
using TasneemSami.ToDoList.Database.GeniricRepository;
using TasneemSami.ToDoList.Database.Models;
using TasneemSami.ToDoList.Services.TaskServices;
using Xunit;

public class TaskServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly Mock<ToDoListDbContext> _dbContextMock;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _dbContextMock = new Mock<ToDoListDbContext>();

        
        _taskService = new TaskService(
            _mapperMock.Object,
            _dbContextMock.Object,
            _httpContextAccessorMock.Object
        );
    }

    [Fact]
    public void GetAll_WithUserId_ReturnsFilteredTasks()
    {
     
        var userId = 1;
        var fakeTasks = new List<Tasks>
        {
            new Tasks { ID = 1, TITLE = "Task 1", USERID = userId, ISCOMPLETED = false, PRIORITY = 3 },
            new Tasks { ID = 2, TITLE = "Task 2", USERID = userId, ISCOMPLETED = true, PRIORITY = 1 },
        }.AsQueryable();

      
        var tasksDbSetMock = new Mock<DbSet<Tasks>>();
        tasksDbSetMock.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(fakeTasks.Provider);
        tasksDbSetMock.As<IQueryable<Tasks>>().Setup(m => m.Expression).Returns(fakeTasks.Expression);
        tasksDbSetMock.As<IQueryable<Tasks>>().Setup(m => m.ElementType).Returns(fakeTasks.ElementType);
        tasksDbSetMock.As<IQueryable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(fakeTasks.GetEnumerator());

        _dbContextMock.Setup(c => c.Set<Tasks>()).Returns(tasksDbSetMock.Object);

        _mapperMock.Setup(m => m.ProjectTo<TaskGetOutPutDto>(It.IsAny<IQueryable<Tasks>>(), null))
            .Returns((IQueryable<Tasks> source, object _) =>
                source.Select(t => new TaskGetOutPutDto
                {
                    Id = t.ID,
                    Title = t.TITLE,
                    IsCompleted = t.ISCOMPLETED,
                    Priority = t.PRIORITY
                }));

        var input = new TaskSearchInput
        {
            UserId = userId,
            Take = 10,
            Skip = 0
        };


        var result = _taskService.GetAll(input);

  
        Assert.Equal(2, result.TotalSize);
        Assert.All(result.Items, t => Assert.Equal(userId, userId));
    }
}
