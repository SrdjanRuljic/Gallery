using System;

namespace Application.Contacts.Commands.Update
{
    public static class UpdateContactCommandValidator
    {
        public static bool IsValid(this UpdateContactCommand model, out string validationMessage)
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
