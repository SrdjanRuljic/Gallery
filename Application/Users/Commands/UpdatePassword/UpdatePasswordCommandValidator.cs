using System;

namespace Application.Users.Commands.UpdatePassword
{
    public static class UpdatePasswordCommandValidator
    {
        public static bool IsValid(this UpdatePasswordCommand model, out string validationMessage)
        {
            validationMessage = null;
            bool isValid = true;

            if (model == null)
            {
                validationMessage = "Model ne može biti null. ";
                isValid = false;
            }

            if (model.Id == "0" && String.IsNullOrWhiteSpace(model.Password))
            {
                validationMessage += "Neophodno je unijeti lozinku korisnika. ";
                isValid = false;
            }

            if (model.Id == "" && String.IsNullOrWhiteSpace(model.ConfirmedPassword))
            {
                validationMessage += "Neophodno je unijeti potvrdu lozinke korisnika. ";
                isValid = false;
            }

            if (String.Compare(model.Password, model.ConfirmedPassword) != 0)
            {
                validationMessage += "Lozinke se razlikuju.. ";
                isValid = false;
            }

            return isValid;
        }
    }
}
