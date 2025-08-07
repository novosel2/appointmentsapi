using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices;

public interface IAppointmentsService
{
    /// <summary>
    /// Get all appointments for a specific date.
    /// </summary>
    /// <param name="date">Date of appointments</param>
    /// <returns>List of appointment responses with specified date</returns>
    public Task<List<AppointmentResponse>> GetAppointmentsAsync(DateOnly date);

    /// <summary>
    /// Gets an appointment by its ID.
    /// </summary>
    /// <param name="appointmentId">ID of appointment</param>
    /// <returns>Appointment response</returns>
    public Task<AppointmentResponse> GetByIdAsync(Guid appointmentId);

    /// <summary>
    /// Adds a new appointment.
    /// </summary>
    /// <param name="request">Appointment to be added</param>
    /// <returns>Added appointment</returns>
    public Task<AppointmentResponse> AddAppointmentAsync(AppointmentAddRequest request);

    /// <summary>
    /// Delete an appointment by id
    /// </summary>
    /// <param name="appointmentId">Id of appointment</param>
    /// <returns>Deleted appointment object</returns>
    public Task<AppointmentResponse> DeleteAppointmentAsync(Guid appointmentId);

    /// <summary>
    /// Update an appointment with new information
    /// </summary>
    /// <param name="request">Updated appointment</param>
    /// <returns>Updated appointment</returns>
    public Task<AppointmentResponse> UpdateAppointmentAsync(AppointmentUpdateRequest request);
}
