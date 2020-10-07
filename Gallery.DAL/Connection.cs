namespace Gallery.DAL
{
    internal class Connection
    {
        private readonly string connectionString = "Data Source=.\\MSSQLSERVER2017;Initial Catalog=Gallery;Integrated Security=true;User Id=rulja;Password=armor;";

        public string ConnectionString
        {
            get { return connectionString; }
        }
    }
}
