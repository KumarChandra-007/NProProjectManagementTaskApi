namespace SampleProjectMgmt.ResponseDTO
{
    public class TaskManagementDTO
    {
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Deadline { get; set; }
        public int ProjectID { get; set; }
        public int TaskCount { get; set; }

    }

    public class TaskManagementResponse
    {
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Deadline { get; set; }
        public int ProjectID { get; set; }
        public int TaskCount { get; set; }

    }
}
