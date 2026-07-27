using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomAuthorizationLoginExample.Database.AppDbContextModels;

namespace CustomAuthorizationLoginExample.Domain.Features.Login
{
    public class LoginService
    {
        private readonly AppDbContext _db;
        public LoginService(AppDbContext db)
        {
            _db = db;
        }
        public async Task <LoginResponseModel>HandleAsync(LoginRequestModel requestModel)
        {
            try
            {
                var user = _db.TblUsers.FirstOrDefault(x =>
                x.UserName == requestModel.UserName &&
                x.Password == requestModel.Password);

                if (user is null)
                {
                    throw new Exception("Invalid Username or password");
                }
                TblLogin login = new TblLogin
                {
                    SessionId = Guid.NewGuid().ToString(),
                    SessionExpired = DateTime.Now.AddMinutes(30),
                    UserId = user.UserId
                };
                _db.TblLogins.Add(login);
                await _db.SaveChangesAsync();
                return new LoginResponseModel
                {
                    UserId = user.UserId.ToString(),
                    SessionId = login.SessionId,
                    SessionExpired = login.SessionExpired
                };
            } 
            catch (Exception ex)
            {
                throw;
            }

        }
    }
    public class LoginRequestModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class  LoginResponseModel
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public DateTime SessionExpired { get; set; }

    }
}
