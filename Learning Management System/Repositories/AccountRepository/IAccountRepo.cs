using Learning_Management_System.Models;

namespace Learning_Management_System.Repositories.AccountRepository
{
    public interface IAccountRepo
    {
        public Task<User> RegisterAsync(RegisterModel model);
        public Task<string> LoginAsync(LoginModel model);
    }
}
