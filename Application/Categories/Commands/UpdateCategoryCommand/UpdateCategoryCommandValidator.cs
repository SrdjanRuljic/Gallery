using System;

namespace Application.Categories.Commands.UpdateCategoryCommand
{
    public static class UpdateCategoryCommandValidator
    {
        public static bool IsValid(this UpdateCategoryCommand model, out string validationMessage)
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
