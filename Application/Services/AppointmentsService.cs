using Application.Exceptions;
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

    public async Task<AppointmentResponse> GetByIdAsync(Guid appointmentId)
    {
        Appointment? appointment = await _appointmentsRepository.GetByIdAsync(appointmentId);

        if (appointment == null)
        {
            throw new NotFoundException($"Appointment not found. ID {appointmentId}");
        }

        return appointment.ToResponse();
    }

    public async Task<AppointmentResponse> AddAppointmentAsync(AppointmentAddRequest request)
    {
        Appointment appointment = request.ToAppointment();
        Appointment addedAppointment = await _appointmentsRepository.AddAppointmentAsync(appointment);

        if (!await _appointmentsRepository.IsSavedAsync())
        {
            throw new SavingChangesFailedException("Failed to save the appointment.");
        }

        return addedAppointment.ToResponse();
    }

    public async Task<AppointmentResponse> DeleteAppointmentAsync(Guid appointmentId) 
    {
        Appointment appointment = await _appointmentsRepository.GetByIdAsync(appointmentId)
            ?? throw new NotFoundException($"Appointment not found. ID {appointmentId}");

        _appointmentsRepository.DeleteAppointment(appointment);

        if (!await _appointmentsRepository.IsSavedAsync()) 
        {
            throw new SavingChangesFailedException("Failed to delete appointment.");
        }

        return appointment.ToResponse();
    }

    public async Task<AppointmentResponse> UpdateAppointmentAsync(AppointmentUpdateRequest request)
    {
        Appointment appointment = await _appointmentsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException($"Appointment not found. ID {request.Id}");

        Appointment updatedAppointment = request.ToAppointment();

        _appointmentsRepository.UpdateAppointment(appointment, updatedAppointment);

        if (!await _appointmentsRepository.IsSavedAsync())
        {
            throw new SavingChangesFailedException("Failed to update appointment.");
        }

        return updatedAppointment.ToResponse();
    }
}
