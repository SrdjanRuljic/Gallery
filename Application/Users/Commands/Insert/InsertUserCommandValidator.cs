using System;

namespace Application.Users.Commands.Insert
{
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

            if (String.IsNullOrWhiteSpace(model.RoleId))
            {
                validationMessage += "Neophodno je odabrati ulogu. ";
                isValid = false;
            }

            return isValid;
        }
    }
}
