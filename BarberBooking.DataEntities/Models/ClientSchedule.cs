namespace BarberBooking.Models.Models;

public class ClientSchedule
{
    public int Id { get; set; } 
    public int AppointmentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; } 

    public Appointment? Appointment { get; set; }
}