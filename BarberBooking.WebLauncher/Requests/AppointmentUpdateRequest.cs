using BarberBooking.Common.Enums;

namespace BarberBooking.WebLauncher.Requests
{
    public class AppointmentUpdateRequest
    {
        public int BarberId { get; set; }
        public string ServiceDescription { get; set; } = null!;
        public DateTime ScheduledDate { get; set; }
     
    }
}
