using System;

namespace Application.Products.Commands.Update
{
    public static class UpdateProductCommandValidator
    {
        public static bool IsValid(this UpdateProductCommand model, out string validationMessage)
        {
            validationMessage = null;
            bool isValid = true;


            if (model == null)
            {
                validationMessage = "Model proizvoda ne može biti null.";
                isValid = false;
            }

            if (String.IsNullOrWhiteSpace(model.Name))
            {
                validationMessage += "Neophodno je unijeti naziv proizvoda.";
                isValid = false;
            }

            if (model.CategoryId <= 0)
            {
                validationMessage += "Neophodno je odabrati kategoriju. ";
                isValid = false;
            }

            return isValid;
        }
    }
}
