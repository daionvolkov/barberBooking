namespace BarberBooking.Models.Dto
{
    public class DtoClientSchedule
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
