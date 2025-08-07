using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto;

public class AppointmentResponse
{
    public Guid AppointmentId { get; set; }
    public string? ClientFirstName { get; set; }
    public string? ClientLastName { get; set; }
    public string? AppointmentType { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public decimal Cost { get; set; }
}

public static class AppointmentExtension
{
    public static AppointmentResponse ToResponse(this Appointment appointment)
    {
        return new AppointmentResponse
        {
            AppointmentId = appointment.Id,
            ClientFirstName = appointment.ClientFirstName,
            ClientLastName = appointment.ClientLastName,
            AppointmentType = appointment.AppointmentType,
            Date = appointment.Date,
            StartTime = appointment.StartTime,
            EndTime = appointment.EndTime,
            Cost = appointment.Cost
        };
    }
}
