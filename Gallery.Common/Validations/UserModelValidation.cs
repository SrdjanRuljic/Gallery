using System;

namespace Gallery.Common.Validations
{
    public static class UserModelValidation
    {
        public static bool IsValid(this UserModel model, out string validationMessage)
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
