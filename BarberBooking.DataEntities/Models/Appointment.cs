using BarberBooking.Common.Enums;

namespace BarberBooking.Models.Models;

public class Appointment
{
    public int Id { get; set; } 
    public int ClientId { get; set; } 
    public int BarberId { get; set; } 
    public ServiceDescriptionEnum ServiceDescription { get; set; }
    public DateTime ScheduledDate { get; set; } 
    public AppointmentStatusEnum Status { get; set; } = AppointmentStatusEnum.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Client? Client { get; set; }
    
    public Barber? Barber { get; set; }
    public ICollection<ClientSchedule>? ClientSchedules { get; set; } = new List<ClientSchedule>();
}