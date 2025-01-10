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
public class WorkScheduleController : Controller
{
  private readonly BarberDbContext _context;
  private readonly WorkScheduleManager _workScheduleManager;

  public WorkScheduleController(BarberDbContext context)
  {
    _context = context;
    _workScheduleManager = new WorkScheduleManager(context);  
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<DtoWorkSchedule>>> GetWorkSchedule()
  {
    var workSchedules = await _workScheduleManager.GetWorkSchedulesAsync();
    var result = workSchedules.Select(c =>
    {
      var dtoWorkSchedules = ObjectCopier.CopyProperties<WorkSchedule, DtoWorkSchedule>(c);
      return dtoWorkSchedules;
    });
    return Ok(result);
  }

  [HttpGet("{id:int}")]
  public async Task<ActionResult<DtoWorkSchedule>> GetWorkSchedule(int id)
  {
    var workSchedule = await _workScheduleManager.GetWorkScheduleByIdAsync(id);
    if (workSchedule == null)
    {
      return NotFound($"Work Schedule with ID: {id} not found");
    }
    var result = ObjectCopier.CopyProperties<WorkSchedule, DtoWorkSchedule>(workSchedule);
    return Ok(result);
  }

  [HttpPost]
  public async Task<ActionResult<DtoWorkSchedule>> PostWorkSchedule(WorkScheduleRequest request)
  {
    var workSchedules = ObjectCopier.CopyProperties<WorkScheduleRequest, BoWorkSchedule>(request);
    var createdWorkSchedule = await _workScheduleManager.CreateWorkScheduleAsync(workSchedules);
    var result = ObjectCopier.CopyProperties<WorkSchedule, DtoWorkSchedule>(createdWorkSchedule);
    return Ok(result);
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult<DtoWorkSchedule>> PutWorkSchedule(int id, WorkScheduleRequest request)
  {
    var workSchedules = ObjectCopier.CopyProperties<WorkScheduleRequest, BoWorkSchedule>(request);
    var updatedWorkSchedule = await _workScheduleManager.UpdateWorkScheduleAsync(id, workSchedules);
    if (updatedWorkSchedule == null)
    {
      return NotFound($"Work Schedule with ID: {id} not found");
    }
    var result = ObjectCopier.CopyProperties<WorkSchedule, DtoWorkSchedule>(updatedWorkSchedule);
    return Ok(result);
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult<bool>> DeleteWorkSchedule(int id)
  {
    var workSchedule = await _workScheduleManager.DeleteWorkScheduleAsync(id);
    return  workSchedule == true ? Ok(workSchedule) : NotFound();
    
  }
}