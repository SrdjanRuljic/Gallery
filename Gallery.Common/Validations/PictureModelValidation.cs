using System;
using System.Collections.Generic;
using System.Text;

namespace Gallery.Common.Validations
{
    public static class PictureModelValidation
    {
        public static bool IsValid(this PictureModel model, out string validationMessage)
        {
            validationMessage = null;
            bool isValid = true;


            if (model == null)
            {
                validationMessage = "Model slike ne može biti null.";
                isValid = false;
            }

            if (String.IsNullOrWhiteSpace(model.Name))
            {
                validationMessage += "Neophodno je unijeti naziv slike.";
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
