namespace BarberBooking.WebLauncher.Requests
{
    public class ClientCreateRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
    }
}
