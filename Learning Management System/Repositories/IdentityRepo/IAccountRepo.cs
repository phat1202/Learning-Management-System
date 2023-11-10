using Learning_Management_System.Repositories.ModelRepositories;
using Microsoft.AspNetCore.Identity;

namespace Learning_Management_System.Repositories.IdentityRepo
{
    public interface IAccountRepo
    {
        public Task<IdentityResult> RegisterAsync(RegisterModel model);
        public Task<string> LoginAsync(LoginModel model);
    }
}
