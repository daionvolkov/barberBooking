namespace BarberBooking.Models.Models;

public class Client
{
    public int Id { get; init; } 
    public string FirstName { get; init; } = null!; 
    public string LastName { get; init; } = null!; 
    public string PhoneNumber { get; set; } = null!; 
    public string? Email { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
}