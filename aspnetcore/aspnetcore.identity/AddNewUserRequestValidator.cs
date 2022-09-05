using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace aspnetcore.identity;

public class AddNewUserRequestValidator : AbstractValidator<AddNewUserRequest>
{
    public AddNewUserRequestValidator(MyDbContext context)
    {
        RuleFor(x => x.UserName).NotNull().Length(3, 10)
            .Must(x => !context.Users.Any(u => u.UserName == x))
            .WithMessage("User name existed");
        RuleFor(x => x.Email).NotNull().EmailAddress()
            .Must(x => x.EndsWith("163.com") || x.EndsWith("qq.com"));
        RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
    }
}