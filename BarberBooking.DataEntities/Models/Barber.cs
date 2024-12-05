using BarberBooking.Common.Enums;

namespace BarberBooking.Models.Models;

public class Barber
{
    public int Id { get; set; } 
    public string FullName { get; set; } = null!;
    public BarberSpecializationEnum Specialization { get; set; } 
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; } 
    public int ExperienceYears { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
    public ICollection<WorkSchedule>? WorkSchedules { get; set; } = new List<WorkSchedule>();
}