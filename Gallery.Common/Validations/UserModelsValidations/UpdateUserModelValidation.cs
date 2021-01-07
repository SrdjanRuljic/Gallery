using Gallery.Common.UserModels;
using System;

namespace Gallery.Common.Validations.UserModelsValidations
{
    public static class UpdateUserModelValidation
    {
        public static bool IsValid(this UpdateUserModel model, out string validationMessage)
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

            if (model.RoleId <= 0)
            {
                validationMessage += "Neophodno je odabrati ulogu. ";
                isValid = false;
            }

            return isValid;
        }
    }
}
