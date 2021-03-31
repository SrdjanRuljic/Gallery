using System;

namespace Application.Categories.Commands.Insert
{
    public static class InsertCategoryCommandValidator
    {
        public static bool IsValid(this InsertCategoryCommand model, out string validationMessage)
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
