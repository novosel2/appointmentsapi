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

    [HttpGet("{date}")]
    public async Task<IActionResult> GetAppointments(DateOnly date)
    {
        List<AppointmentResponse> appointments = await _appointmentsService.GetAppointmentsAsync(date);
        return Ok(appointments);
    }

    [HttpPost]
    public async Task<IActionResult> AddAppointment(AppointmentAddRequest request)
    {
        AppointmentResponse appointment = await _appointmentsService.AddAppointmentAsync(request);
        return Ok(appointment);
    }
}
