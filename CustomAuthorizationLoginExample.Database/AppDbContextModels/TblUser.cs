using System;
using System.Collections.Generic;

namespace CustomAuthorizationLoginExample.Database.AppDbContextModels;

public partial class TblUser
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;
    public string UserName { get; set; }
    public string Email { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string Password { get; set; } = null!;
}
