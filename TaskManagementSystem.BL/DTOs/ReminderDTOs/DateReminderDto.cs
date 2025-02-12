namespace TaskManagementSystem.BL.DTOs.ReminderDTOs
{
    public class DateReminderDto
    {
        public DateTime TimeBefore { get; set; }
        public string Message { get; set; }
        public int AppointmentId { get; set; }
    }
}
