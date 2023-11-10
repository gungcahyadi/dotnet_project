namespace project3.Services;

using project3.Entities;
using project3.Models.Users;
using project3.Helpers;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

public interface IUserService
{
    IEnumerable<User> All();
    User FindByUsername(string username);
    User Find(int id);
    User Create(CreateRequest model);
    void Update(int id, UpdateRequest model);
    void Delete(int id);
}

public class UserService : IUserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<User> All()
    {
        return _context.Users;
    }    

    // find by username
    public User FindByUsername(string username)
    {
        var user = _context.Users.SingleOrDefault(x => x.Username == username);
        if (user == null)
        {
            throw new AppException("User not found");
        }
        return user;
    }

    public User Find(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            throw new AppException("User not found");
        }
        return user;
    }

    public User Create(CreateRequest model)
    {
        // validate
        if (_context.Users.Any(x => x.Username == model.Username))
            throw new AppException("Username '" + model.Username + "' is already taken");
        if (_context.Users.Any(x => x.Email == model.Email))
            throw new AppException("Email '" + model.Email + "' is already taken");

        // map model to new user object
        var user = new User
        {
            Name = model.Name,
            Username = model.Username,
            Email = model.Email,
            PasswordHash = BCrypt.HashPassword(model.Password)
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public void Update(int id, UpdateRequest model)
    {
        var user = _context.Users.Find(id);
        // validate
        if (user == null)
            throw new AppException("User not found");
        if (user.Username != model.Username && _context.Users.Any(x => x.Username == model.Username))
            throw new AppException("Username '" + model.Username + "' is already taken");
        if (user.Email != model.Email && _context.Users.Any(x => x.Email == model.Email))
            throw new AppException("Email '" + model.Email + "' is already taken");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
        {
            user.PasswordHash = BCrypt.HashPassword(model.Password);
        }

        // copy model to user and save
        user.Name = model.Name;
        user.Username = model.Username;
        user.Email = model.Email;

        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}