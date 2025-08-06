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
    /// Adds a new appointment.
    /// </summary>
    /// <param name="appointment">Appointment to be added</param>
    /// <returns>Added appointment</returns>
    public Task<Appointment> AddAppointmentAsync(Appointment appointment);
}
