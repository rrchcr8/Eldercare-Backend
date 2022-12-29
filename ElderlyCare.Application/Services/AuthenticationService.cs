using ElderlyCare.Application.Common.Errors;
using ElderlyCare.Application.Common.Interfaces.Authentication;
using ElderlyCare.Application.Common.Interfaces.Persistence;
using ElderlyCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElderlyCare.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // 1.    Check if user already exists
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new DuplicateEmailException();
        }

        // 2.   Create user (generate an unique Id) & persist
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);


        //3.    Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        // 1.    Check if user already exists
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given name does not exists");
        }

        // 2.   validate if the password is correct.
        if (user.Password != password)
        {
            throw new Exception("Invalid password");
        }


        //3.    Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
           user,
           token);
    }
}
