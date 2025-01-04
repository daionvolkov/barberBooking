namespace BarberBooking.WebLauncher.Requests.RequestCommon;

public abstract class HumanCommonRequest
{
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
}