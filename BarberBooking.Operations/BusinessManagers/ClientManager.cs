using BarberBooking.DataAccess.AppContext;
using BarberBooking.Models.Dto;
using BarberBooking.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberBooking.Operations.BusinessManagers;

public class ClientManager
{
    private readonly BarberDbContext _context;

    public ClientManager(BarberDbContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        return client;
    }
    
}