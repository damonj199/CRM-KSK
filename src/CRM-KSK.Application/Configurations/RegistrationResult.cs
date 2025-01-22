namespace CRM_KSK.Application.Configurations;

public class RegistrationResult
{
    public bool Succeeded { get; set; }
    public string ErrorMessage { get; set; }

    public static RegistrationResult Success()
    {
        return new RegistrationResult { Succeeded = true };
    }

    public static RegistrationResult Failure(string errorMessage)
    {
        return new RegistrationResult { Succeeded = false, ErrorMessage = errorMessage };
    }
}
