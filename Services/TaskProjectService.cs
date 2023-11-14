namespace project3.Services;

using project3.Entities;
using project3.Models.Tasks;
using project3.Helpers;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

public interface ITaskProjectService
{
    IEnumerable<Task> All();
    Task Find(int id);
    Task Create(CreateTaskRequest model, string UserId);
    void Update(int id, UpdateTaskRequest model);
    void Delete(int id);
}

public class TaskProjectService : ITaskProjectService
{
    private readonly DataContext _context;
    

    public TaskProjectService(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<Task> All()
    {
        return _context.Tasks.Include(x => x.User).ToList();
    }

    public Task Find(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null)
        {
            throw new AppException("Task not found");
        }
        return task;
    }

    public Task Create(CreateTaskRequest model, string UserId)
    {
        
        var id = int.Parse(UserId);
        var task = new Task
        {
            UserId = id,
            Title = model.Title,
            Description = model.Description
        };

        _context.Tasks.Add(task);
        _context.SaveChanges();

        return task;
    }

    public void Update(int id, UpdateTaskRequest model)
    {
        var task = _context.Tasks.Find(id);
        if (task == null)
        {
            throw new AppException("Task not found");
        }

        // update Task properties
        task.Title = model.Title;
        task.Description = model.Description;

        _context.Tasks.Update(task);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}