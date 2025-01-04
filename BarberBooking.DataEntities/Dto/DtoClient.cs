using BarberBooking.Models.Dto.DtoCommon;

namespace BarberBooking.Models.Dto;

public class DtoClient : DtoHuman
{
    public string FirstName { get; set; } = null!; 
    public string LastName { get; set; } = null!; 
    public DateTime CreatedAt { get; set; }
    public ICollection<DtoAppointment>? Appointments { get; set; } = new List<DtoAppointment>();
}