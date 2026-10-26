namespace ClinicApp.Models
{
    public class UrgentAppointment : Appointment
    {
        private readonly decimal _discountMultiplier;
        public string UrgencyNote { get; }
        public override decimal GetCost()
        {
            return (base.GetCost() * 1.5m) * _discountMultiplier;
        }

        public UrgentAppointment(int patientId, int doctorId, DateTime scheduledAt, string urgencyNote = "", int durationMinutes = 30, decimal discountMultiplier = 1.0m)
            : base(patientId, doctorId, scheduledAt, durationMinutes)
        {
            UrgencyNote = urgencyNote;
            _discountMultiplier = discountMultiplier;
        }

        public override sealed string GetDescription()
        {
            if (string.IsNullOrWhiteSpace(UrgencyNote))
            {
                return "Терміновий";
            }
            return $"Терміновий ({UrgencyNote})";
        }
        public new int GetPriority()
        {
            return 1;
        }


    }
}
