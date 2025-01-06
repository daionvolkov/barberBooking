namespace BarberBooking.WebLauncher.Requests;

public  class ClientScheduleRequest
{
    public int AppointmentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; } 
}