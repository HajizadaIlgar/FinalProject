using AutoMapper;
using TaskManagementSystem.BL.DTOs.DatePlannerDTOs;
using TaskManagementSystem.CORE.Entities.Plans;

namespace TaskManagementSystem.BL.Mapper
{
    public class DatePalannerProfiles : Profile
    {
        public DatePalannerProfiles()
        {
            CreateMap<DateListItem, Appointment>().ReverseMap();
        }
    }
}
