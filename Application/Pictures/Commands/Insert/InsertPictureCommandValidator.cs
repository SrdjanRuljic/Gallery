﻿using System;

namespace Application.Pictures.Commands.Insert
{
    public static class InsertPictureCommandValidator
    {
        public static bool IsValid(this InsertPictureCommand model, out string validationMessage)
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