using BarberBooking.WebLauncher.Requests.RequestCommon;

namespace BarberBooking.WebLauncher.Requests;

public class BarberCreateRequest : HumanCommonRequest
{
   
    public string FullName { get; set; } = null!;
    public string? Specialization { get; set; } 
    public int ExperienceYears { get; set; } = 0;
}