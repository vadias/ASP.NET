using AppAuthLesson4.Contracts.Requests;

namespace AppAuthLesson4.Abstractions
{
    public interface IAuthService
    {
        Task<IResult> Login(UserAuthRequest request);
        Task<IResult> Register(UserAuthRequest request);
    }
}
