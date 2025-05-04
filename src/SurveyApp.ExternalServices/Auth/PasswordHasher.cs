﻿using SurveyApp.ExternalServices.Abstractions;

namespace SurveyApp.ExternalServices.Auth;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string plainPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }
    public bool Verify(string plainPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }
}