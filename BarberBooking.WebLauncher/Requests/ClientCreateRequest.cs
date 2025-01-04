using BarberBooking.WebLauncher.Requests.RequestCommon;

namespace BarberBooking.WebLauncher.Requests
{
    public class ClientCreateRequest : HumanCommonRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
