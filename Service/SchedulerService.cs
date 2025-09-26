using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SchedulerService : IScheduler
    {
        private readonly ITaskRepository taskRepository;

        public SchedulerService(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public List<TaskItem> allTasks()
        {
            return taskRepository.GetAll();
        }

        public List<ScheduleTrack> OptimizeSchedule(List<TaskItem> tasks)
        {
            var sortedTasks = tasks.OrderBy(t => t.StartTime).ToList();
            var allTracks = new List<ScheduleTrack>();
            int trackCounter = 1;

            foreach (var task in sortedTasks)
            {
                bool placed = false;

                foreach (var track in allTracks)
                {
                    var lastTask = track.Tasks.Last();

                    // ✅ Only place if there's no overlap
                    if (lastTask.EndTime < task.StartTime)
                    {
                        track.Tasks.Add(task);
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    var newTrack = new ScheduleTrack
                    {
                        Id = Guid.NewGuid(),
                        TrackNumber = trackCounter++,
                        Tasks = new List<TaskItem> { task }
                    };

                    allTracks.Add(newTrack);
                }
            }

            return allTracks;
        }


    }
}
