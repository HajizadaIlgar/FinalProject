using TaskManagementSystem.CORE.Entities.Commons;
using TaskManagementSystem.CORE.Entities.Notfications;
using TaskManagementSystem.CORE.Enums;

namespace TaskManagementSystem.CORE.Entities.Plans
{
    public class Appointment : BaseEntity
    {
        public string DateName { get; set; }           // Görüşün adı
        public string? Description { get; set; }     // Görüş haqqında məlumat
        public DateTime StartDate { get; set; }     // Başlama tarixi və saatı
        public DateTime EndDate { get; set; }       // Bitmə tarixi və saatı
        public string? Location { get; set; }        // Görüşün yeri
        public List<DateReminder> Reminders { get; set; } // Görüşə xəbərdarlıq
        public DateStatus Status { get; set; } = DateStatus.Scheduled; // Görüşün statusu
        public string Host { get; set; }            // Görüşün təşkilatçısı
        public string? Notes { get; set; }           // Qeydlər
    }



}
