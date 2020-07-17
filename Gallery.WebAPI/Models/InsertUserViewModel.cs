namespace Gallery.WebAPI.Models
{
    public class InsertUserViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public long RoleId { get; set; }
        public string Password { get; set; }
    }
}
