using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Model
{
    public class CommentManagement
    {
        public int CommentID { get; set; }
        public int TaskID { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
        public string Deadline { get; set; }

        [ForeignKey("TaskID")]
        public TaskManagement TaskManagement { get; set; } // Navigation property for TaskManagement
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public UserManagement UserManagement { get; set; } // Navigation property for UserManagement


    }
}
