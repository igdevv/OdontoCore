using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Application.Exceptions
{
    public sealed class AppointmentConflictException: Exception
    {
        public AppointmentConflictException() : base("Dentist already has an appointment at this time.")
        {
        }
    }
}
