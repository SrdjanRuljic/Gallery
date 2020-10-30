using System;

namespace Gallery.Common.Validations
{
    public static class AboutAuthorValidation
    {
        public static bool IsValid(this AboutAuthorModel model, out string validationMessage)
        {
            validationMessage = null;
            bool isValid = true;


            if (model == null)
            {
                validationMessage = "Model autora ne može biti null.";
                isValid = false;
            }

            if (String.IsNullOrWhiteSpace(model.Name))
            {
                validationMessage += "Neophodno je unijeti naziv autora.";
                isValid = false;
            }

            return isValid;
        }
    }
}
