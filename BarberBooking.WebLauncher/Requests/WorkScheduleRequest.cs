namespace BarberBooking.WebLauncher.Requests;

public class WorkScheduleRequest
{
    public int BarberId { get; set; } 
    public int DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; } 
}