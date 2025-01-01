using BarberBooking.Common.Enums;

namespace BarberBooking.Models.Models;

public class WorkSchedule
{
    public int Id { get; set; } 
    public int ScheduleId { get; set; } 
    public int BarberId { get; set; } 
    public string? DayOfWeek { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; } 
    
    public Barber? Barber { get; set; }
}