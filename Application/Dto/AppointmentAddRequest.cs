using Application.Validation;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto;
public class AppointmentAddRequest
{
    public string? ClientFirstName { get; set; }
    public string? ClientLastName { get; set; }
    public string? AppointmentType { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    [TimeRange(nameof(StartTime))]
    public TimeOnly EndTime { get; set; }

    public decimal Cost { get; set; }

    public Appointment ToAppointment()
    {
        return new Appointment
        {
            Id = Guid.NewGuid(),
            ClientFirstName = ClientFirstName,
            ClientLastName = ClientLastName,
            AppointmentType = AppointmentType,
            Date = Date,
            StartTime = StartTime,
            EndTime = EndTime,
            Cost = Cost
        };
    }
}
