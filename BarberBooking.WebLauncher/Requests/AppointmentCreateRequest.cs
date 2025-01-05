using BarberBooking.Common.Enums;

namespace BarberBooking.WebLauncher.Requests
{
    public class AppointmentCreateRequest
    {
        public int ClientId { get; set; }
        public int BarberId { get; set; }
        public string ServiceDescription { get; set; } = null!;
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; } = AppointmentStatusEnum.Pending.ToString();
    }
}
