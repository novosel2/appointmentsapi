using Application.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class AppointmentsRepository : IAppointmentsRepository
{
    private readonly AppDbContext _db;

    public AppointmentsRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Appointment>> GetAppointmentsAsync(DateOnly date)
    {
        return await _db.Appointments
            .Where(a => a.Date == date)
            .OrderBy(a => a.StartTime)
            .ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(Guid appointmentId)
    {
        return await _db.Appointments.FirstOrDefaultAsync(a => a.Id == appointmentId);
    }

    public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
    {
        var result = await _db.Appointments.AddAsync(appointment);
        return result.Entity;
    }

    public void DeleteAppointment(Appointment appointment) 
    {
        _db.Appointments.Remove(appointment);
    }

    public void UpdateAppointment(Appointment appointment, Appointment updatedAppointment)
    {
        _db.Entry<Appointment>(appointment).CurrentValues.SetValues(updatedAppointment);
        _db.Entry<Appointment>(appointment).State = EntityState.Modified;
    }

    public async Task<bool> IsSavedAsync()
    {
        int saved = await _db.SaveChangesAsync();
        return saved > 0;
    }

    
}
