using SampleProjectMgmt.ResponseDTO;
using TaskApi.Model;

namespace ProjectManagement.Interfaces
{
    public interface ITaskManagementServices
    {
        Task<List<TaskManagementDTO>> GetTaskDetails();
        Task<List<TaskManagementDTO>> GetTaskDetailById(int id);
        Task<List<TaskManagementDTO>> GetTaskDetailByProjectId(int Projectid);
        Task<List<ProjectBasedTaskCount>> GetTaskCount();
        Task<List<TaskManagementDTO>> SaveTaskDetail(List<TaskManagementDTO> taskManagementDTO);
        Task DeleteTaskById(int id);
    }
}
