namespace ddd.usermanager.domain;

public enum UserAccessResult
{
    PhoneNumberNotFound,
    Lockout,
    NoPassword,
    OK,
    PasswordError
}
