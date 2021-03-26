using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    //public class InsertUserCommandValidator : AbstractValidator<InsertUserCommand>
    //{
    //    public InsertUserCommandValidator()
    //    {
    //        RuleFor(x => x.Password).NotEmpty()
    //                                .WithMessage("Neophodno je unijeti lozinku korisnika. ");
    //        RuleFor(x => x.Username).NotEmpty()
    //                                .WithMessage("Neophodno je unijeti korisničko ime korisnika. ");
    //        RuleFor(x => x.RoleId).GreaterThan(0)
    //                              .WithMessage("Neophodno je odabrati ulogu. ");
    //    }
    //}

    public static class InsertUserCommandValidator
    {
        public static bool IsValid(this InsertUserCommand model, out string validationMessage)
        {
            validationMessage = null;
            bool isValid = true;

            if (model == null)
            {
                validationMessage = "Model korisnika ne može biti null. ";
                isValid = false;
            }

            if (String.IsNullOrWhiteSpace(model.Username))
            {
                validationMessage += "Neophodno je unijeti korisničko ime korisnika. ";
                isValid = false;
            }

            if (model.Id == 0 && String.IsNullOrWhiteSpace(model.Password))
            {
                validationMessage += "Neophodno je unijeti lozinku korisnika. ";
                isValid = false;
            }

            if (model.RoleId <= 0)
            {
                validationMessage += "Neophodno je odabrati ulogu. ";
                isValid = false;
            }

            return isValid;
        }
    }
}
