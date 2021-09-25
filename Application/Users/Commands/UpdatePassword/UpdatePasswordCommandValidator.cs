using System;
using System.Text.RegularExpressions;

namespace Application.Users.Commands.UpdatePassword
{
    public static class UpdatePasswordCommandValidator
    {
        public static bool IsValid(this UpdatePasswordCommand model, out string validationMessage)
        {
            validationMessage = null;
            bool isValid = true;
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$";

            if (model == null)
            {
                validationMessage = "Model ne može biti null. ";
                isValid = false;
            }

            if (String.IsNullOrWhiteSpace(model.Password))
            {
                validationMessage += "Neophodno je unijeti lozinku korisnika. ";
                isValid = false;
            }

            if (String.IsNullOrWhiteSpace(model.ConfirmedPassword))
            {
                validationMessage += "Neophodno je unijeti potvrdu lozinke korisnika. ";
                isValid = false;
            }

            if (model.Password.Length < 6)
            {
                validationMessage += "Neophodno je da lozinka korisnika sadrži minimum 6 karaktera. ";
                isValid = false;
            }

            if (!Regex.IsMatch(model.Password, passwordPattern))
            {
                validationMessage += "Lozinka mora sadržavati velika i mala slova, brojeve i specijalne karaktere. ";
                isValid = false;
            }

            if (String.Compare(model.Password, model.ConfirmedPassword) != 0)
            {
                validationMessage += "Lozinke se razlikuju. ";
                isValid = false;
            }

            return isValid;
        }
    }
}
