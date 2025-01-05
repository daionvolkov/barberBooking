using BarberBooking.Common.Helpers;
using BarberBooking.DataAccess.AppContext;
using BarberBooking.Models.Models;
using BarberBooking.Operations.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace BarberBooking.Operations.BusinessManagers;

public class BarberManager
{
    private readonly BarberDbContext _context;

    public BarberManager(BarberDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Barber>> GetBarbersAsync()
    {
        var barbers = await _context.Barbers!
            .Include(b => b.Appointments)
           .Include(b => b.WorkSchedules)
            .ToListAsync();
        return barbers;
    }

    public async Task<Barber?> GetBarberByIdAsync(int id)
    {
        var barber = await _context.Barbers!
            .Include(b => b.Appointments)
            .Include(b => b.WorkSchedules)
            .FirstOrDefaultAsync(c => c.Id == id);
        return barber;
    }
    
     public async Task<Barber> CreateBarberAsync(BoBarber boBarber)
    {
        var barber = ObjectCopier.CopyProperties<BoBarber, Barber>(boBarber);
        var result = await _context.Barbers!.AddAsync(barber);
        await _context.SaveChangesAsync();

        return result.Entity;
    }


    public async Task<Barber?> UpdateBarberAsync(int id, BoBarber boBarber)
    {
        var barber = await GetBarberByIdAsync(id);
        if (barber == null) 
        {
            return null;
        }
        barber.PhoneNumber = boBarber.PhoneNumber;
        barber.Email = boBarber.Email;

        var result =  _context.Barbers!.Update(barber);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<bool> DeleteBarberAsync(int id)
    {
        var barber = await GetBarberByIdAsync(id);
        if (barber == null)
        {
            return false;
        }
        _context.Barbers!.Remove(barber);
        await _context.SaveChangesAsync();
        return true;
    }
}