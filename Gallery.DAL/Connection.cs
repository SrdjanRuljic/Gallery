namespace Gallery.DAL
{
    internal class Connection
    {
        private string connectionString = "Data Source=DESKTOP-TO1UCBV\\MSSQLSERVER2017;Initial Catalog=Gallery;Integrated Security=true;User Id=rulja;Password=rulja;";

        public string ConnectionString
        {
            get { return connectionString; }
        }
    }
}
