namespace BarberBooking.Models.Dto;

public class DtoClient
{
    public int Id { get; set; } 
    public string FirstName { get; set; } = null!; 
    public string LastName { get; set; } = null!; 
    public string PhoneNumber { get; set; } = null!; 
    public string? Email { get; set; } 
    public DateTime CreatedAt { get; set; }

    public ICollection<DtoAppointment>? Appointments { get; set; } = new List<DtoAppointment>();
}