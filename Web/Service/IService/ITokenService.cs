namespace Web.Service.IService {
    public interface ITokenService {
        void ClearToken();
        void SetToken(string token);
        string? GetToken();
    }
}