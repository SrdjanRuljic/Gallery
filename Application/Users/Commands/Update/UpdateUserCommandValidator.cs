using System;

namespace Application.Users.Commands.Update
{
    public static class UpdateUserCommandValidator
    {
        public static bool IsValid(this UpdateUserCommand model, out string validationMessage)
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

            if (String.IsNullOrWhiteSpace(model.RoleId))
            {
                validationMessage += "Neophodno je odabrati ulogu. ";
                isValid = false;
            }

            return isValid;
        }
    }
}
