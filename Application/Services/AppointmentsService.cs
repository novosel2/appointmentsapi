using Application.IRepositories;
using Application.IServices;
using Domain.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class AppointmentsService : IAppointmentsService
{
    private readonly IAppointmentsRepository _appointmentsRepository;

    public AppointmentsService(IAppointmentsRepository appointmentsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
    }


    public async Task<List<AppointmentResponse>> GetAppointmentsAsync(DateOnly date)
    {
        List<Appointment> appointments = await _appointmentsRepository.GetAppointmentsAsync(date);

        List<AppointmentResponse> responses = appointments.Select(a => a.ToResponse()).ToList();

        return responses;
    }

    public async Task<AppointmentResponse> AddAppointmentAsync(AppointmentAddRequest request)
    {
        Appointment appointment = request.ToAppointment();

        Appointment addedAppointment = await _appointmentsRepository.AddAppointmentAsync(appointment);

        return addedAppointment.ToResponse();
    }
}
