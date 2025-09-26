using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Web.DTOs;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduler _scheduler;
        private readonly ITaskRepository _taskRepository;

        public ScheduleController(IScheduler scheduler, ITaskRepository taskRepository)
        {
            _scheduler = scheduler;
            _taskRepository = taskRepository;
        }

        // POST: api/schedule/store
        [HttpPost("store")]
        public async Task<IActionResult> StoreTasks([FromBody] List<TaskItemDto> taskDtos)
        {
            var tasks = taskDtos.Select(dto => new TaskItem
            {
                Title = dto.Title,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            }).ToList();

           // await _taskRepository.ClearAsync();
            await _taskRepository.AddRangeAsync(tasks);

            return Ok("Tasks stored successfully.");
        }

        // GET: api/schedule/partition
        [HttpGet("partition")]
        public async Task<ActionResult<List<ScheduleTrackDto>>> GetPartitionedSchedule()
        {
            var tasks = await _taskRepository.GetAllAsync();
            var tracks = _scheduler.OptimizeSchedule(tasks);

            var result = tracks.Select(track => new ScheduleTrackDto
            {
                TrackNumber = track.TrackNumber,
                Tasks = track.Tasks.Select(t => new TaskItemDto
                {
                    Title = t.Title,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<TaskItem>>> all()
        {
            return _taskRepository.GetAll();
        }

    }
}
