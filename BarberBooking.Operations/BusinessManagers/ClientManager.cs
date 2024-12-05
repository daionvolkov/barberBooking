using BarberBooking.DataAccess.AppContext;

namespace BarberBooking.Operations.BusinessManagers;

public class ClientManager
{
    private readonly BarberDbContext _context;

    public ClientManager(BarberDbContext context)
    {
        _context = context;
    }
}