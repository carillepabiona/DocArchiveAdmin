namespace DocArchiveAdmin.DTOs
{
    public class UserCreateDto
    {
        public string UserCode { get; set; }      // NEW
        public string Username { get; set; }
        public string Password { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }         // NEW
        public string ContactNumber { get; set; } // NEW
        public string Address { get; set; }       // NEW

        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool ForcePasswordChangeOnLogin { get; set; }

        public int? AccessLevel { get; set; }
    }
}
