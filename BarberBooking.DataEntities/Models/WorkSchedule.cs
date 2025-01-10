using BarberBooking.Common.Enums;

namespace BarberBooking.Models.Models;

public class WorkSchedule
{
    public int Id { get; init; } 
    public int BarberId { get; set; } 
    public int DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; } 
    public Barber Barber { get; set; } = null!;
}