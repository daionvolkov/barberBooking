using BarberBooking.Operations.BusinessObjects.BoCommonObjects;

namespace BarberBooking.Operations.BusinessObjects;

public class BoBarber : BoHuman
{
    public string FullName { get; set; } = null!;
    public int ExperienceYears { get; set; } = 0;
    public string? Specialization { get; set; } 
}