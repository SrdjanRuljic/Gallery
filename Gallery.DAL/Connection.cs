namespace Gallery.DAL
{
    internal class Connection
    {
        private string connectionString = "Data Source=DESKTOP-5N65NA1;Initial Catalog=Gallery;Integrated Security=true;User Id=rulja;Password=armor;";

        public string ConnectionString
        {
            get { return connectionString; }
        }
    }
}
