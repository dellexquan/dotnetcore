namespace ddd.usermanager.domain;

public enum CheckCodeResult
{
    PhoneNumberNotFound,
    Lockout,
    CodeError,
    OK
}