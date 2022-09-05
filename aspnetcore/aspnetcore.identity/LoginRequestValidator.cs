using FluentValidation;

namespace aspnetcore.identity;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).NotNull().EmailAddress()
            .Must(v => v.EndsWith("@qq.com") || v.EndsWith("@163.com"))
            .WithMessage("Only support qq and 163 mailbox!");
        RuleFor(x => x.Password).NotNull().Length(3, 10)
            .WithMessage("Password length should be between 3 and 10")
            .Equal(x => x.ConfirmPassword).WithMessage("Confirm password should be same as password!");
    }
}