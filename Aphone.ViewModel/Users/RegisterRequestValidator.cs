using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aphone.ViewModel.Users
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Bắt buộc nhập họ")
                .MaximumLength(200).WithMessage("Họ không quá 200 ký tự");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Bắt buộc nhập tên")
                .MaximumLength(200).WithMessage("Tên không quá 200 ký tự");

            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Không thể nhập quá 100 tuổi");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Bắt buộc nhập Email")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Email format not match");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Bắt buộc nhập SĐT");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Bắt buộc nhập tên tài khoản");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Bắt buộc nhập mật khẩu")
                .MinimumLength(6).WithMessage("Mật khẩu ít nhất 6 ký tự");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Không trùng khớp mật khẩu");
                }
            });
        }
    }
}
