namespace BarberBooking.Models.Dto;

public class DtoWorkSchedule
{
    public int Id { get; set; } 
    public int BarberId { get; set; } 
    public int DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; } 
}