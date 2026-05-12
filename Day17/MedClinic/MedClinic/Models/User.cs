namespace MedClinic.Models
{
    public enum UserRole { Doctor, Patient }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }

        public override string ToString() => $"{FullName} ({Role})";
    }
}