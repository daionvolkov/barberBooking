using BarberBooking.Common.Enums;

namespace BarberBooking.Models.Models;

public class Appointment
{
    public int Id { get; set; } 
    public int ClientId { get; set; } 
    public int BarberId { get; set; } 
    public string? ServiceDescription { get; init; }
    public DateTime ScheduledDate { get; init; } 
    public string Status { get; set; } = AppointmentStatusEnum.Pending.ToString();
    public DateTime CreatedAt { get; set; }
    public Client? Client { get; set; }
    public Barber? Barber { get; set; }
    public ICollection<ClientSchedule>? ClientSchedules { get; set; } = new List<ClientSchedule>();
}