using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public static class ErrorMessages
    {
        private static string incorectUsernameOrPassword;
        private static string userExists;
        private static string categoryExists;
        private static string canNotDeleteCategory;
        private static string idCanNotBeLowerThanOne;
        private static string categoryNotFound;
        private static string canNotDeleteUser;
        private static string userNotFound;
        private static string dataNotFound;
        private static string pictureNotFound;
        private static string contactNotFound;
        private static string inernalServerError;
        private static string unauthorised;
        private static string authorNotFound;
        private static string passwordsAreDifferent;

        public static string Unauthorised
        {
            get { return inernalServerError = "Nemate pravo pristupa."; }
        }

        public static string InernalServerError
        {
            get { return inernalServerError = "Greška na serverskoj strani, molimo obratite se administratoru."; }
        }


        public static string DataNotFound
        {
            get { return dataNotFound = "Traženi podaci nisu pronađeni."; }
        }


        public static string UserNotFound
        {
            get { return userNotFound = "Traženi korisnik nije pronađen."; }
        }

        public static string CanNotDeleteUser
        {
            get { return canNotDeleteUser = "Nije moguće obrisati korisnika."; }
        }

        public static string CategoryNotFound
        {
            get { return categoryNotFound = "Tražena kategorija nije pronađena."; }
        }

        public static string IdCanNotBeLowerThanOne
        {
            get { return idCanNotBeLowerThanOne = "ID ne može biti manji od 1."; }
        }

        public static string UserExists
        {
            get { return userExists = "Korisnik sa unešenim korisničkim imenom već postoji, molimo unesite drugo korisničko ime."; }
        }

        public static string CategoryExists
        {
            get { return categoryExists = "Kategorija već postoji, molimo unesite drugi naziv."; }
        }

        public static string CanNotDeleteCategory
        {
            get { return canNotDeleteCategory = "Nije moguće obrisati kategoriju."; }
        }

        public static string IncorectUsernameOrPassword
        {
            get { return incorectUsernameOrPassword = "Neispravno korisničko ime ili lozinka."; }
        }

        public static string PictureNotFound
        {
            get { return pictureNotFound = "Tražena slika nije pronađena."; }
        }

        public static string ContactNotFound
        {
            get { return contactNotFound = "Traženi kontakt nije pronađena."; }
        }

        public static string AuthorNotFound
        {
            get { return authorNotFound = "Traženi podaci o autoru nisu pronađeni."; }
        }

        public static string GetMessageForHttpCode(int code)
        {
            switch (code)
            {
                case 401:
                    return Unauthorised;
                default:
                    return InernalServerError;
            }
        }

        public static string PasswordsAreDifferent
        {
            get { return inernalServerError = "Lozinke se razlikuju. "; }
        }
    }
}
