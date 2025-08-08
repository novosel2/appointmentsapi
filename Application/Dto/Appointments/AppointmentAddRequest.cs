using Application.Validation;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto;
public class AppointmentAddRequest
{
    public string? ClientFirstName { get; set; }
    public string? ClientLastName { get; set; }
    public string? AppointmentType { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    [TimeRange(nameof(StartTime))]
    public TimeOnly EndTime { get; set; }

    [Required]
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
