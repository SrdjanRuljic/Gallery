using System;

namespace Application.Contacts.Commands.Insert
{
    public static class InsertContactCommandValidator
    {
        public static bool IsValid(this InsertContactCommand model, out string validationMessage)
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
