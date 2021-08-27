using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aphone.ViewModel.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Bắt buộc nhập tên tài khoản");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Bắt buộc nhập mật khẩu")
                .MinimumLength(6).WithMessage("Mật khẩu ít nhất 6 ký tự");
        }
    }
}
