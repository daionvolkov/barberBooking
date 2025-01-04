using BarberBooking.Models.Dto.DtoCommon;

namespace BarberBooking.Models.Dto;

public class DtoBarber : DtoHuman
{
    public string FullName { get; set; } = null!;
    public string? Specialization { get; set; } 
    public int ExperienceYears { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
    public ICollection<DtoAppointment>? Appointments { get; set; } = new List<DtoAppointment>();
    public ICollection<DtoWorkSchedule>? WorkSchedules { get; set; } = new List<DtoWorkSchedule>();
}