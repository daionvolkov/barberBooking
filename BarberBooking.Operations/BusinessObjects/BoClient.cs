
using BarberBooking.Operations.BusinessObjects.BoCommonObjects;

namespace BarberBooking.Operations.BusinessObjects
{
    public class BoClient : BoHuman
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
       
    }
}
