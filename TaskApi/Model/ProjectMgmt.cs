using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Model
{
    public partial class ProjectMgmt
    {
        [Key]
        public int ProjectID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Deadline { get; set; }

        // Change CreatorID to int to match UserDetails' primary key type
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public UserManagement UserManagement { get; set; }

        public ICollection<TaskManagement> Tasks { get; set; }
    }
}
