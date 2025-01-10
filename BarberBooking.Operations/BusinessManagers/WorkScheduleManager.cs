using BarberBooking.Common.Helpers;
using BarberBooking.DataAccess.AppContext;
using BarberBooking.Models.Models;
using BarberBooking.Operations.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace BarberBooking.Operations.BusinessManagers;

public class WorkScheduleManager
{
    private readonly BarberDbContext _context;

    public WorkScheduleManager(BarberDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<WorkSchedule>> GetWorkSchedulesAsync()
    {
        var workSchedules = await _context.WorkSchedules!.ToListAsync();
        return workSchedules;
    }

    public async Task<WorkSchedule?> GetWorkScheduleByIdAsync(int id)
    {
        var workSchedule = await _context.WorkSchedules!.FirstOrDefaultAsync(ws => ws.Id == id);
        return workSchedule;
    }

    public async Task<WorkSchedule> CreateWorkScheduleAsync(BoWorkSchedule boWorkSchedule)
    {
       var workSchedule = ObjectCopier.CopyProperties<BoWorkSchedule, WorkSchedule>(boWorkSchedule);
       var result =  _context.WorkSchedules.Add(workSchedule);
       await _context.SaveChangesAsync();
       return result.Entity;
    }

    public async Task<WorkSchedule?> UpdateWorkScheduleAsync(int id, BoWorkSchedule boWorkSchedule)
    {
        var workSchedule = await GetWorkScheduleByIdAsync(id);
        if (workSchedule == null)
        {
            return null;
        }
        workSchedule.BarberId = boWorkSchedule.BarberId;
        workSchedule.DayOfWeek = boWorkSchedule.DayOfWeek;
        workSchedule.StartTime = boWorkSchedule.StartTime;
        workSchedule.EndTime = boWorkSchedule.EndTime;
        
        _context.Entry(workSchedule).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return workSchedule;
    }


    public async Task<bool> DeleteWorkScheduleAsync(int id)
    {
        var workSchedule = await GetWorkScheduleByIdAsync(id);
        if (workSchedule == null)
        {
            return false;
        }
        _context.WorkSchedules!.Remove(workSchedule);
        return await _context.SaveChangesAsync() > 0;
    }
}