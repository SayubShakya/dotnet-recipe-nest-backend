﻿using MessagePack;

namespace RecipeNest.Request;

[MessagePackObject]
public class CreateUserRequest
{
    public CreateUserRequest()
    {
    }

    public CreateUserRequest(string firstName, string lastName, string phoneNumber, string email, string password,
        int roleId, string? imageUrl, string? about)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        ImageUrl = imageUrl;
        About = about;
        Email = email;
        Password = password;
        RoleId = roleId;
    }

    [Key("first_name")] public string FirstName { get; set; }

    [Key("last_name")] public string LastName { get; set; }

    [Key("phone_number")] public string PhoneNumber { get; set; }

    [Key("image_url")] public string? ImageUrl { get; set; }

    [Key("about")] public string? About { get; set; }

    [Key("email")] public string Email { get; set; }

    [Key("password")] public string Password { get; set; }

    [Key("role_id")] public int RoleId { get; set; }

    public override string ToString()
    {
        return $"CreateUserRequest: Name={FirstName} {LastName}, Email={Email}, RoleId={RoleId}";
    }
}