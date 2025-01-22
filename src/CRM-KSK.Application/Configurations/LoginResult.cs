namespace CRM_KSK.Application.Configurations;

public class LoginResult
{
    public bool Succeeded { get; set; }
    public string ErrorMessage { get; set; }
    public string Token { get; set; }

    public static LoginResult Success(string token)
    {
        return new LoginResult { Succeeded = true, Token = token };
    }

    public static LoginResult Failure(string errorMessage)
    {
        return new LoginResult { Succeeded = false, ErrorMessage = errorMessage };
    }
}
