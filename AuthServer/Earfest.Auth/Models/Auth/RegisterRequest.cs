﻿namespace Earfest.Auth.Models.Auth;

public record RegisterRequest(string Email, string Password, string? FirstName, string? LastName);