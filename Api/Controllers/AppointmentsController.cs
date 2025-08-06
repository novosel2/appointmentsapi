using Application.IServices;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers;

[ApiController]
[Route("appointments")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentsService _appointmentsService;

    public AppointmentsController(IAppointmentsService appointmentsService)
    {
        _appointmentsService = appointmentsService;
    }


    // GET: /appointments/date/2025-08-06

    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetAppointments(DateOnly date)
    {
        List<AppointmentResponse> appointments = await _appointmentsService.GetAppointmentsAsync(date);
        return Ok(appointments);
    }


    // GET: /appointments/811ea307-b212-4b9e-8b8d-0105433bb590

    [HttpGet("{appointmentId}")]
    public async Task<IActionResult> GetAppointmentById(Guid appointmentId)
    {
        AppointmentResponse appointment = await _appointmentsService.GetByIdAsync(appointmentId);
        return Ok(appointment);
    }


    // POST: /appointments

    [HttpPost]
    public async Task<IActionResult> AddAppointment(AppointmentAddRequest request)
    {
        AppointmentResponse appointment = await _appointmentsService.AddAppointmentAsync(request);
        return Ok(appointment);
    }


    // DELETE: /appointments/{appointmentId}

    [HttpDelete("{appointmentId}")]
    public async Task<IActionResult> DeleteAppointment(Guid appointmentId) 
    {
        AppointmentResponse appointment = await _appointmentsService.DeleteAppointmentAsync(appointmentId);
        return Ok(appointment);
    }
}
