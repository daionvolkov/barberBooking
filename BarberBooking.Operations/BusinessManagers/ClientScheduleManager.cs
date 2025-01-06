using BarberBooking.Common.Helpers;
using BarberBooking.DataAccess.AppContext;
using BarberBooking.Models.Models;
using BarberBooking.Operations.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace BarberBooking.Operations.BusinessManagers;

public class ClientScheduleManager
{
    private readonly BarberDbContext _context;

    public ClientScheduleManager(BarberDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ClientSchedule>> GetClientSchedulesAsync()
    {
        var clientSchedule = await _context.ClientSchedules!.ToListAsync();
        return clientSchedule;
    }

    public async Task<ClientSchedule?> GetClientScheduleByIdAsync(int id)
    {
        var clientSchedule = await _context.ClientSchedules!.FirstOrDefaultAsync(x => x.Id == id);
        return clientSchedule;
    }

    public async Task<ClientSchedule> CreateClientScheduleAsync(BoClientSchedule boClientSchedule)
    {
        var clientSchedule = ObjectCopier.CopyProperties<BoClientSchedule, ClientSchedule>(boClientSchedule);
        var result = await _context.ClientSchedules!.AddAsync(clientSchedule);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<ClientSchedule?> UpdateClientScheduleAsync(int id, BoClientSchedule boClientSchedule)
    {
        var clientSchedule = await GetClientScheduleByIdAsync(id);
        if (clientSchedule == null)
        {
            return null;
        }
        clientSchedule.StartTime = boClientSchedule.StartTime;
        clientSchedule.EndTime = boClientSchedule.EndTime;
        _context.Entry(clientSchedule).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return clientSchedule;
    }

    public async Task<bool> DeleteClientScheduleAsync(int id)
    {
        var clientSchedule = await GetClientScheduleByIdAsync(id);
        if (clientSchedule == null)
        {
            return false;
        }
        _context.ClientSchedules!.Remove(clientSchedule);
        return await _context.SaveChangesAsync() > 0;
    }
}