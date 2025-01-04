namespace BarberBooking.Models.Dto.DtoCommon;

public abstract class DtoHuman
{
    public int Id { get; set; } 
   
    public string PhoneNumber { get; set; } = null!; 
    public string? Email { get; set; } 
}