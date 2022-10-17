using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao _userDao;

    public UserLogic(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public async Task<UserBasicDto?> GetByUsernameAsync(string username)
    {
        User? user = await _userDao.GetByUsernameAsync(username);

        return user == null ? null : new UserBasicDto(user.Username);
    }

    public async Task<UserBasicDto> GetByUsernameAndPasswordAsync(string username, string password)
    {
        User? existingUser = await _userDao.GetByUsernameAsync(username);

        if (existingUser == null) throw new Exception("User with this username does not exist");

        if (!existingUser.Password.Equals(password)) throw new Exception("Invalid password");

        return new UserBasicDto(existingUser.Username);
    }

    public async Task<UserBasicDto> CreateAsync(UserCreateDto dto)
    {
        User? existingUser = await _userDao.GetByUsernameAsync(dto.Username);
        if (existingUser != null) throw new Exception("Username already taken!");

        ValidateData(dto);

        User newUser = await _userDao.CreateAsync(new User
        {
            Username = dto.Username,
            Password = dto.Password
        });

        return new UserBasicDto(newUser.Username);
    }

    public async Task<IEnumerable<UserBasicDto>> GetAllAsync()
    {
        IEnumerable<User> users = await _userDao.GetAllAsync();

        return users.Select(user => new UserBasicDto(user.Username));
    }

    private static void ValidateData(UserCreateDto dto)
    {
        string username = dto.Username;

        if (string.IsNullOrEmpty(username)) throw new ValidationException("Username is required");

        if (username.Length < 3) throw new ValidationException("Username must be at least 3 characters!");

        if (username.Length > 15) throw new ValidationException("Username must be less than 16 characters!");

        if (string.IsNullOrEmpty(dto.Password)) throw new ValidationException("Password is required");
    }
}