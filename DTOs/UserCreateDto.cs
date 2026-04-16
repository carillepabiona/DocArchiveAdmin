namespace DocArchiveAdmin.DTOs
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }   // plain password (API will hash)
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool ForcePasswordChangeOnLogin { get; set; }
    }
}
