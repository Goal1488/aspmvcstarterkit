namespace FuckThisNumber.Interfaces
{
    public interface IAuthorizeManager
    {
        IAuthentificateResult Authentificate(ILoginViewModel loginViewModel);
        void LogOut();
    }
}