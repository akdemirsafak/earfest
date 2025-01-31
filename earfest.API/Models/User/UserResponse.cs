﻿namespace earfest.API.Models.User;

public class UserResponse : BaseResponse
{
    public string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
}
