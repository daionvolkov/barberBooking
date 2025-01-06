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

public class ClientScheduleController : Controller
{
    private readonly BarberDbContext _context;
    private readonly ClientScheduleManager _clientScheduleManager;
    private readonly AppointmentManager _appointmentManager;

    public ClientScheduleController(BarberDbContext context)
    {
        _context = context;
        _clientScheduleManager = new ClientScheduleManager(context);
        _appointmentManager = new AppointmentManager(context);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DtoClientSchedule>>> GetClientSchedule()
    {
        var clientSchedule = await _clientScheduleManager.GetClientSchedulesAsync();
        var result = clientSchedule.Select(c =>
        {
            var dtoClient = ObjectCopier.CopyProperties<ClientSchedule, DtoClientSchedule>(c);
            return dtoClient;
        });
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DtoClientSchedule>>GetClientScheduleById(int id)
    {
        var clientSchedule = await _clientScheduleManager.GetClientScheduleByIdAsync(id);
        if (clientSchedule == null)
        {
            return NotFound($"Client Schedule with ID: {id} not found");
        }
        
        var result = ObjectCopier.CopyProperties<ClientSchedule, DtoClientSchedule>(clientSchedule);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<DtoClient>> CreateClient([FromBody] ClientScheduleRequest request) 
    {
        
        var appointment = await _appointmentManager.GetAppointmentByIdAsync(request.AppointmentId);
        if (appointment == null)
        {
            return NotFound($"You don't add to a client's schedule for an appointment that doesn't exist");
        }
        var isDateTimeValid = DateTimeValidator.AreDatesValid(request.StartTime, request.EndTime);
        if (!isDateTimeValid)
        {
            return BadRequest("Invalid start time or end time");
        }
        var client = ObjectCopier.CopyProperties<ClientScheduleRequest, BoClientSchedule>(request);
        var createdClientSchedule = await _clientScheduleManager.CreateClientScheduleAsync(client);
        var result = ObjectCopier.CopyProperties<ClientSchedule, DtoClientSchedule>(createdClientSchedule);

        return Ok(result);
    }

    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<DtoClientSchedule>> UpdateClientSchedule(int id, [FromBody] ClientScheduleRequest request)
    {
        var appointment = await _appointmentManager.GetAppointmentByIdAsync(request.AppointmentId);
        if (appointment == null)
        {
            return NotFound($"You don't add to a client's schedule for an appointment that doesn't exist\n " +
                            $".Appointment with ID: {id} not found");
        }
        var isDateTimeValid = DateTimeValidator.AreDatesValid(request.StartTime, request.EndTime);
        if (!isDateTimeValid)
        {
            return BadRequest("Invalid start time or end time");
        }
        var clientSchedule = ObjectCopier.CopyProperties<ClientScheduleRequest, BoClientSchedule>(request);
        var updatedClientSchedule = await _clientScheduleManager.UpdateClientScheduleAsync(id, clientSchedule);
        if(updatedClientSchedule == null)
        {
            return NotFound($"Client with ID: {id} not found");
        }
        var result = ObjectCopier.CopyProperties<ClientSchedule, DtoClientSchedule>(updatedClientSchedule);

        return Ok(result);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteClientSchedule(int id)
    {
        var result = await _clientScheduleManager.DeleteClientScheduleAsync(id);
        if(result == false)
        {
            return NotFound($"Client Schedule with ID: {id} not found");
        }
        return Ok(result);
    }
}