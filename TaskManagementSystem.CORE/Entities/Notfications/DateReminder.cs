using TaskManagementSystem.CORE.Entities.Plans;

namespace TaskManagementSystem.CORE.Entities.Notfications
{
    public class DateReminder
    {
        public int Id { get; set; }
        public DateTime TimeBefore { get; set; }  // Hər hansı bir xəbərdarlıq üçün vaxt 
        public string Message { get; set; }       // Xəbərdarlıq mesajı
        public int AppointmentId {  get; set; }
        public Appointment? Appointment { get; set; }
    }
}
