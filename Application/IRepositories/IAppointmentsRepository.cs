using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories;

public interface IAppointmentsRepository
{
    /// <summary>
    /// Gets all appointments for a specific date.
    /// </summary>
    /// <param name="date">Date of the appointments</param>
    /// <returns>List of appointments with specified date</returns>
    public Task<List<Appointment>> GetAppointmentsAsync(DateOnly date);

    /// <summary>
    /// Gets an appointment by its ID.
    /// </summary>
    /// <param name="appointmentId">ID of appointment</param>
    /// <returns>Appointment if found, null if not</returns>
    public Task<Appointment?> GetByIdAsync(Guid appointmentId);

    /// <summary>
    /// Adds a new appointment.
    /// </summary>
    /// <param name="appointment">Appointment to be added</param>
    /// <returns>Added appointment</returns>
    public Task<Appointment> AddAppointmentAsync(Appointment appointment);

    /// <summary>
    /// Checks if the changes have been saved to database.
    /// </summary>
    /// <returns>True if any changes are saved, false if not</returns>
    public Task<bool> IsSavedAsync();
}
