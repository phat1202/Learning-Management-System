using Learning_Management_System.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Learning_Management_System.Extensions;

namespace Learning_Management_System.Repositories.AccountRepository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly IConfiguration _configuration;
        private readonly LmsDbContext _context;
        public AccountRepo(IConfiguration configuration, LmsDbContext context)
        {


            _configuration = configuration;
            _context = context;
        }

        public Task<string> LoginAsync(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<User> RegisterAsync(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email.ToUpper(),
                Password = model.Password.Hash(),
                Role = (int)EnumClass.Role.User,
                Gender = model.Gender,
                IsActive = true,
                IsDeleted = false,
                IsTeacher = false,
                IsStudent = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            await _context.AddAsync(user);
            return user;
        }
    }
}
