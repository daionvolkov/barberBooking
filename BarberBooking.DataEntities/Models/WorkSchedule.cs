using BarberBooking.Common.Enums;

namespace BarberBooking.Models.Models;

public class WorkSchedule
{
    public int ScheduleId { get; set; } 
    public int BarberId { get; set; } 
    public DaysOfWeekEnumEnum DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; } 
    
    public Barber? Barber { get; set; }
}