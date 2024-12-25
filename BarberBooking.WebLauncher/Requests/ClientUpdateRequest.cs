namespace BarberBooking.WebLauncher.Requests
{
    public class ClientUpdateRequest
    {
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
    }
}
