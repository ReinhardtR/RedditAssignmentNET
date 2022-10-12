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

    public async Task<User> CreateAsync(UserCreateDto dto)
    {
        User? existingUser = await _userDao.GetByUsernameAsync(dto.Username);
        if (existingUser != null) throw new Exception("Username already taken!");

        ValidateData(dto);

        User userToCreate = new()
        {
            Username = dto.Username
        };

        User newUser = await _userDao.CreateAsync(userToCreate);

        return newUser;
    }

    private static void ValidateData(UserCreateDto userToCreate)
    {
        string username = userToCreate.Username;

        if (username.Length < 3) throw new Exception("Username must be at least 3 characters!");

        if (username.Length > 15) throw new Exception("Username must be less than 16 characters!");
    }
}