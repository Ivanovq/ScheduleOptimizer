namespace Web.DTOs
{
    public class ScheduleTrackDto
    {
        public int TrackNumber { get; set; }
        public List<TaskItemDto> Tasks { get; set; } = new();
    }
}
