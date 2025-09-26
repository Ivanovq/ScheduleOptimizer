using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ScheduleTrack:BaseEntity
    {
        public int TrackNumber { get; set; }
        public List<TaskItem> Tasks { get; set; } = new();
    }
}
