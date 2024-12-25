using BarberBooking.Common.Helpers;
using BarberBooking.DataAccess.AppContext;
using BarberBooking.Models.Models;
using BarberBooking.Operations.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace BarberBooking.Operations.BusinessManagers;

public class ClientManager
{
    private readonly BarberDbContext _context;

    public ClientManager(BarberDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetClients()
    {
        var clients = await _context.Clients!
            .Include(c => c.Appointments)
            .ToListAsync();
        return clients;
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        var client = await _context.Clients!
            .Include(c => c.Appointments)
            .FirstOrDefaultAsync(c => c.Id == id);
        return client;
    }

    public async Task<Client> CreateClientAsync(BoClient boClient)
    {
        var client = ObjectCopier.CopyProperties<BoClient, Client>(boClient);
        var result = await _context.Clients!.AddAsync(client);
        await _context.SaveChangesAsync();

        return result.Entity;
    }


    public async Task<Client?> UpdateClientAsync(int id, BoClient boClient)
    {
        var client = await GetClientByIdAsync(id);
        if (client == null) 
        {
            return null;
        }
        client.PhoneNumber = boClient.PhoneNumber;
        client.Email = boClient.Email;

        var result =  _context.Clients!.Update(client);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<bool> DeleteClientAsync(int id)
    {
        var client = await GetClientByIdAsync(id);
        if (client == null)
        {
            return false;
        }
        _context.Clients!.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }
}
    
