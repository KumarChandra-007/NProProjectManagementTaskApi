using Microsoft.EntityFrameworkCore;
using ProjectManagement.DBContext;
using ProjectManagement.Interfaces;
using ProjectManagement.Model;
using SampleProjectMgmt.ResponseDTO;
using TaskApi.Model;

namespace ProjectManagement.Services
{
    public class TaskManagementServices : ITaskManagementServices
    {
        private readonly DBConnection _unitOfWork;

        public TaskManagementServices(DBConnection unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<TaskManagementDTO>> GetTaskDetails()
        {
            try
            {
                List<TaskManagementDTO> result = await _unitOfWork.TaskManagement
            //.Where(task => task.Status == "Completed")
            .OrderByDescending(task => task.Deadline)
            .Select(task => new TaskManagementDTO
            {
                TaskID = task.TaskID,
                Title = task.Title,
                Deadline = task.Deadline,
                ProjectID = task.ProjectID,
                Status = task.Status,
                Description = task.Description
                
            })
            .ToListAsync();

                int totalCount = result.Count;
                result.ForEach(dto => dto.TaskCount = totalCount);

                return result;

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TaskManagementDTO>> GetTaskDetailById(int id)
        {
            try
            {
                List<TaskManagementDTO> result = await _unitOfWork.TaskManagement
                    .Where(task => task.TaskID == id)
                    .OrderByDescending(task => task.Deadline)
                    .Select(task => new TaskManagementDTO
                    {
                        TaskID = task.TaskID,
                        Title = task.Title,
                        Deadline = task.Deadline,
                        ProjectID = task.ProjectID,
                        Status = task.Status,
                        Description = task.Description
                    })
                    .ToListAsync();

                int totalCount = result.Count;
                result.ForEach(dto => dto.TaskCount = totalCount);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProjectBasedTaskCount>> GetTaskCount() { 
             var result=_unitOfWork.TaskManagement.GroupBy(p => p.ProjectID)
                   .Select(g => new ProjectBasedTaskCount{ ProjectId = g.Key, TaskCount = g.Count() });
            return result.ToList();
        }
        public async Task<List<TaskManagementDTO>> GetTaskDetailByProjectId(int Projectid)
        {
            try
            {
                List<TaskManagementDTO> result = await _unitOfWork.TaskManagement
                    .Where(task => task.ProjectID == Projectid)
                    .OrderByDescending(task => task.Deadline)
                    .Select(task => new TaskManagementDTO
                    {
                        TaskID = task.TaskID,
                        Title = task.Title,
                        Deadline = task.Deadline,
                        ProjectID = task.ProjectID,
                        Status = task.Status,
                        Description = task.Description
                    })
                    .ToListAsync();

                int totalCount = result.Count;
                result.ForEach(dto => dto.TaskCount = totalCount);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TaskManagementDTO>> SaveTaskDetail(List<TaskManagementDTO> taskManagementDTOs)
        {
            var result = new List<TaskManagementDTO>();

            try
            {
                // Check if taskManagementDTOs is null or empty
                if (taskManagementDTOs == null || !taskManagementDTOs.Any())
                {
                    // Set default error response for each task
                    foreach (var taskDto in taskManagementDTOs)
                    {
                        result.Add(new TaskManagementDTO { Status = "Task data is null." });
                    }
                    return result;
                }

                foreach (var taskManagementDTO in taskManagementDTOs)
                {
                    // Check if TaskID is zero (indicating a new task)
                    if (taskManagementDTO.TaskID == 0)
                    {
                        // Save new task
                        var newTask = new TaskManagement
                        {
                            Title = taskManagementDTO.Title,
                            Deadline = taskManagementDTO.Deadline,
                            ProjectID = taskManagementDTO.ProjectID,
                            Status = taskManagementDTO.Status,
                            Description = taskManagementDTO.Description,
                            Signoff = taskManagementDTO.Signoff
                            
                    };

                        // Add new task to the database
                        _unitOfWork.TaskManagement.Add(newTask);
                        

                        // Return the saved task DTO
                        result.Add(new TaskManagementDTO
                        {
                            TaskID = newTask.TaskID,
                            Title = newTask.Title,
                            Deadline = newTask.Deadline,
                            ProjectID = newTask.ProjectID,
                            Status = newTask.Status,
                            Description = newTask.Description,
                            Signoff = newTask.Signoff
                        });
                    }
                    else
                    {
                        // Update existing task
                        var existingTask =  _unitOfWork.TaskManagement.Find(taskManagementDTO.TaskID);
                        if (existingTask == null)
                        {
                            // Set default error response if task is not found
                            result.Add(new TaskManagementDTO { Status = $"Task with ID {taskManagementDTO.TaskID} not found." });
                            continue; // Move to the next task
                        }

                        // Update task properties
                        existingTask.Title = taskManagementDTO.Title;
                        existingTask.Deadline = taskManagementDTO.Deadline;
                        existingTask.ProjectID = taskManagementDTO.ProjectID;
                        existingTask.Status = taskManagementDTO.Status;
                        existingTask.Description = taskManagementDTO.Description;
                        existingTask.Signoff = taskManagementDTO.Signoff;

                        // Update task in the database
                        _unitOfWork.TaskManagement.Update(existingTask);
                        _unitOfWork.SaveChanges();

                        // Return the updated task DTO
                        result.Add(new TaskManagementDTO
                        {
                            TaskID = existingTask.TaskID,
                            Title = existingTask.Title,
                            Deadline = existingTask.Deadline,
                            ProjectID = existingTask.ProjectID,
                            Status = existingTask.Status,
                            Description = existingTask.Description,
                            Signoff = existingTask.Signoff
                        });
                    }
                }
                _unitOfWork.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                // Handle exceptions and set error response for each task
                foreach (var taskDto in taskManagementDTOs)
                {
                    result.Add(new TaskManagementDTO { Status = ex.Message });
                }
                return result;
            }
        }


        public async Task DeleteTaskById(int id)
        {
            try
            {
                // Retrieve the task by ID from the database context
                var task = await _unitOfWork.TaskManagement.FindAsync(id);

                if (task == null)
                {
                    throw new Exception("Task not found");
                }

                // Remove the task from the context
                _unitOfWork.TaskManagement.Remove(task);

                // Save changes to the database
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
