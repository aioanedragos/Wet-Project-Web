
namespace wet_ui.Services
{
    public interface IAuthService
    {
        public Task RegisterPerson(UserRegistrationDto person);

        public Task<string> LoginPerson(UserLoginDto person);
    }
}
