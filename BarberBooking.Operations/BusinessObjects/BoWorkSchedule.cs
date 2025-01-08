namespace BarberBooking.Operations.BusinessObjects
{
    public class BoWorkSchedule
    {
        public int BarberId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
