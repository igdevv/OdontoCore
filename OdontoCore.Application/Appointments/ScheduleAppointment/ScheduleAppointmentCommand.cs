using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Application.Appointments.ScheduleAppointment
{
    public sealed record ScheduleAppointmentCommand(
        Guid PatientId,
        Guid DentistId,
        Guid DentalServiceId,
        DateTime StartsAt);

    ///<summary>
        ///Datos que necesita una cita para crearse! 
    ///</summary>
    

}
