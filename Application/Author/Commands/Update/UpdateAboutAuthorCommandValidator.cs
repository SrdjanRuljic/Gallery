using System;

namespace Application.Author.Commands.Update
{
    public static class UpdateAboutAuthorCommandValidator
    {
        public static bool IsValid(this UpdateAboutAuthorCommand model, out string validationMessage)
        {
            validationMessage = null;

            if (model == null)
            {
                validationMessage = "Model autora ne može biti null.";
                return false;
            }

            if (String.IsNullOrWhiteSpace(model.Name))
            {
                validationMessage = "Neophodno je unijeti naziv autora.";
                return false;
            }

            return true;
        }
    }
}
