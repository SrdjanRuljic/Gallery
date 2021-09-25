using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.Users.Commands.Insert
{
    public static class InsertUserCommandValidator
    {
        public static bool IsValid(this InsertUserCommand model, out string validationMessage)
        {
            validationMessage = null;
            bool isValid = true;
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$";

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

            if (!model.Username.All(x => Char.IsLetterOrDigit(x) || x == '-' || x == '.' || x == '_' || x == '@' || x == '+'))
            {
                validationMessage += "Korisničko ime može sadržavati samo slova ili brojeve. ";
                isValid = false;
            }

            if (String.IsNullOrWhiteSpace(model.Password))
            {
                validationMessage += "Neophodno je unijeti lozinku korisnika. ";
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

            if (String.IsNullOrWhiteSpace(model.RoleId))
            {
                validationMessage += "Neophodno je odabrati ulogu. ";
                isValid = false;
            }

            return isValid;
        }
    }
}
