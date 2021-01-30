namespace Gallery.Common
{
    public class UpdatePasswordModel
    {
        public long Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
