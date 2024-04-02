using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagement.Interfaces;
using ProjectManagement.Services;
using SampleProjectMgmt.ResponseDTO;

namespace ProjectManagement.Controllers
{
    [Route("taskapi")]
    [ApiController]
    public class TaskManagementController : ControllerBase
    {
        private readonly ITaskManagementServices _taskManagementServices;

        public TaskManagementController(ITaskManagementServices taskManagementServices)
        {
            _taskManagementServices = taskManagementServices;
        }

        // GET: api/TaskManagement/GetTaskDetails
        [HttpGet("GetTaskDetails")]
        public async Task<ActionResult<List<TaskManagementDTO>>> GetTaskDetails()
        {
            try
            {
                // Retrieve the list of task details from the service
                var taskDetails = await _taskManagementServices.GetTaskDetails();
                // Return the task details as a success response
                return Ok(taskDetails);
            }
            catch (Exception ex)
            {
                // Return an error response if an exception occurs during processing
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/TaskManagement/GetTaskDetailById?id={id}
        [HttpGet("GetTaskDetailById")]
        public async Task<ActionResult<List<TaskManagementDTO>>> GetTaskDetailById(int id)
        {
            try
            {
                // Retrieve the task details by ID from the service
                var taskDetails = await _taskManagementServices.GetTaskDetailById(id);
                // Return the task details as a success response
                return Ok(taskDetails);
            }
            catch (Exception ex)
            {
                // Return an error response if an exception occurs during processing
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetTaskDetailByProjectId/{Projectid}")]
        public async Task<ActionResult<List<TaskManagementDTO>>> GetTaskDetailByProjectId(int Projectid)
        {
            try
            {
                // Retrieve the task details by ID from the service
                var taskDetails = await _taskManagementServices.GetTaskDetailByProjectId(Projectid);
                // Return the task details as a success response
                return Ok(taskDetails);
            }
            catch (Exception ex)
            {
                // Return an error response if an exception occurs during processing
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetTaskCount")]
        public async Task<ActionResult<List<TaskManagementDTO>>> GetTaskCount()
        {
            try
            {
                // Retrieve the task details by ID from the service
                var taskDetails = await _taskManagementServices.GetTaskCount();
                // Return the task details as a success response
                return Ok(taskDetails);
            }
            catch (Exception ex)
            {
                // Return an error response if an exception occurs during processing
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/TaskManagement/SaveTaskDetail
        [HttpPost("SaveTaskDetail")]
        public async Task<ActionResult> SaveTaskDetail(TaskManagementDTO taskManagementDTO)
        {
            try
            {
                // Save the task details using the provided DTO
                await _taskManagementServices.SaveTaskDetail(taskManagementDTO);
                // Return a success response
                return Ok("Task saved successfully");
            }
            catch (Exception ex)
            {
                // Return an error response if an exception occurs during processing
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/TaskManagement/DeleteTaskById?id={id}
        [HttpDelete("DeleteTaskById")]
        public async Task<ActionResult> DeleteTaskById(int id)
        {
            try
            {
                // Delete the task by ID
                await _taskManagementServices.DeleteTaskById(id);
                // Return a success response
                return Ok("Task deleted successfully");
            }
            catch (Exception ex)
            {
                // Return an error response if an exception occurs during processing
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}





