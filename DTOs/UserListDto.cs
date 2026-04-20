using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocArchiveAdmin.DTOs
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string UserCode { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }     // "Active" / "Inactive"
        public string Role { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
