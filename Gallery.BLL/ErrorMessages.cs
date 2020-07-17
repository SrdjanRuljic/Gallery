namespace Gallery.BLL
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
    }
}
