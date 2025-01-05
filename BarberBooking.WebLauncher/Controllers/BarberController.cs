using BarberBooking.Common.Helpers;
using BarberBooking.DataAccess.AppContext;
using BarberBooking.Models.Dto;
using BarberBooking.Models.Models;
using BarberBooking.Operations.BusinessManagers;
using BarberBooking.Operations.BusinessObjects;
using BarberBooking.WebLauncher.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BarberBooking.WebLauncher.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BarberController : Controller
{
    private readonly BarberDbContext _context;
    private readonly BarberManager _barberManager;

    public BarberController(BarberDbContext context)
    {
        _context = context;
        _barberManager = new BarberManager(context);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DtoBarber>>> GetBarbers()
    {
        var barbers = await _barberManager.GetBarbersAsync();
        var result = barbers.Select(b =>
        {
            var dtoBarbers = ObjectCopier.CopyProperties<Barber, DtoBarber>(b);

            if (b.Appointments != null)
            {
                dtoBarbers.Appointments = b.Appointments.Select(ObjectCopier.CopyProperties<Appointment, DtoAppointment>).ToList();
            }
            if (b.WorkSchedules != null)
            {
                dtoBarbers.WorkSchedules = b.WorkSchedules.Select(ObjectCopier.CopyProperties<WorkSchedule, DtoWorkSchedule>).ToList();
            }
            
            return dtoBarbers;
        });
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DtoBarber>> GetBarberById(int id)
    {
        var barber = await _barberManager.GetBarberByIdAsync(id);
        if (barber == null)
        {
            return NotFound($"Barber with ID: {id} not found");
        }

        var result = ObjectCopier.CopyProperties<Barber, DtoBarber>(barber);
        if(barber.Appointments != null)
        {
            result.Appointments = barber.Appointments
                .Select(ObjectCopier.CopyProperties<Appointment, DtoAppointment>).ToList();
        }
        if(barber.WorkSchedules != null)
        {
            result.WorkSchedules = barber.WorkSchedules
                .Select(ObjectCopier.CopyProperties<WorkSchedule, DtoWorkSchedule>).ToList();
        }

        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<DtoBarber>> CreateBarber(BarberCreateRequest request) 
    {
        var barber = ObjectCopier.CopyProperties<BarberCreateRequest, BoBarber>(request);
        var createdBarber = await _barberManager.CreateBarberAsync(barber);
        var result = ObjectCopier.CopyProperties<Barber, DtoBarber>(createdBarber);

        return Ok(result);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<DtoBarber>> UpdateBarber(int id, [FromBody] BarberUpdateRequest request)
    {
        var client = ObjectCopier.CopyProperties<BarberUpdateRequest, BoBarber>(request);
        var updatedBarber = await _barberManager.UpdateBarberAsync(id, client);
        if(updatedBarber == null)
        {
            return NotFound($"Barber with ID: {id} not found");
        }
        var result = ObjectCopier.CopyProperties<Barber, DtoBarber>(updatedBarber);

        return Ok(result);
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteBarber(int id)
    {
        var result = await _barberManager.DeleteBarberAsync(id);
        if(result == false)
        {
            return NotFound($"Barber with ID: {id} not found");
        }
        return Ok(result);
    }    
}