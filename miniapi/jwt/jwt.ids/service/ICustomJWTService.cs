namespace jwt.ids.service;

public interface ICustomJWTService
{
    string GetToken(CurrentUser user);
}