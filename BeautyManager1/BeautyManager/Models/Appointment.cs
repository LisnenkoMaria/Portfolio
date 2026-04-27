using System;
using System.ComponentModel.DataAnnotations;

namespace BeautyManager.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public string ClientPhone { get; set; }

        public string ClientEmail { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string AppointmentTime { get; set; }

        public string Notes { get; set; }

        public int ServiceId { get; set; }
        public int MasterId { get; set; }

        public Service Service { get; set; }
        public Master Master { get; set; }
    }
}