namespace CrasAPI.Services.Errors
{
    public enum ErrorCode
    {
        None,
        RecordNotFound,
        UsernameAlreadyExists,
        PasswordTooWeak,
        IncorrectCredentials,
        UserDeactivated,
        UserBlocked
    }
}
