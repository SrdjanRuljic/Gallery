namespace Gallery.WebAPI.Models
{
    public class UpdatePasswordViewModel
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}
