using System;

namespace Gallery.Common.Validations
{
    public static class CategoryModelValidation
    {
        public static bool IsValid(this CategoryModel model, out string validationMessage)
        {
            validationMessage = null;

            if (model == null)
            {
                validationMessage = "Model kategorije ne može biti null.";
                return false;
            }

            if (String.IsNullOrWhiteSpace(model.Name))
            {
                validationMessage = "Neophodno je unijeti naziv kategorije.";
                return false;
            }

            return true;
        }
    }
}
