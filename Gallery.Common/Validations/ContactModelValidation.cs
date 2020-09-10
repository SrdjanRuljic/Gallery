using System;
using System.Collections.Generic;
using System.Text;

namespace Gallery.Common.Validations
{
    public static class ContactModelValidation
    {
        public static bool IsValid(this ContactModel model, out string validationMessage)
        {
            validationMessage = null;

            if (model == null)
            {
                validationMessage = "Model kontakta ne može biti null.";
                return false;
            }

            if (String.IsNullOrWhiteSpace(model.Name))
            {
                validationMessage = "Neophodno je unijeti naziv kontakta.";
                return false;
            }

            if (String.IsNullOrWhiteSpace(model.Value))
            {
                validationMessage = "Neophodno je unijeti vrijednost kontakta.";
                return false;
            }

            return true;
        }
    }
}
