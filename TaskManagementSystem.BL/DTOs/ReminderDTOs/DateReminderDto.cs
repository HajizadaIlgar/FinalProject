using TaskManagementSystem.CORE.Entities.Plans;

namespace TaskManagementSystem.BL.DTOs.ReminderDTOs
{
    public class DateReminderDto
    {
        public DateTime TimeBefore { get; set; }
        public string Message { get; set; }
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public int AppointmentId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
