﻿using TaskManagementSystem.CORE.Entities.Notfications;
using TaskManagementSystem.CORE.Enums;

namespace TaskManagementSystem.BL.DTOs.DatePlannerDTOs
{
    public class DateCreateDto
    {
        public string DateName { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public List<DateReminder> Reminders { get; set; }
        public DateStatus Status { get; set; }
        public string Host { get; set; }
        public string? Notes { get; set; }
    }
}
