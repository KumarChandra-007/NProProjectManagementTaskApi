using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Model
{
    public partial class TaskManagement
    {
        [Key]
        public int TaskID { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Deadline { get; set; }
        public int ProjectID { get; set; }
        //[ForeignKey("ProjectID")]
        //public ProjectMgmt Project { get; set; } // Navigation property
        public bool Signoff { get; set; }
    }
}
