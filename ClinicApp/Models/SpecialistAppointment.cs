namespace ClinicApp.Models
{
    public sealed class SpecialistAppointment : Appointment
    {
        private readonly decimal _discountMultiplier;
        public string ConsultationTopic { get; }

        public SpecialistAppointment(int patientId, int doctorId, DateTime scheduledAt, string consultationTopic = "", int durationMinutes = 45, decimal discountMultiplier = 1.0m)
            : base(patientId, doctorId, scheduledAt, durationMinutes)
        {
            ConsultationTopic = consultationTopic;
            _discountMultiplier = discountMultiplier;
        }

        public override decimal GetCost()
        {
            return (base.GetCost() * 1.3m) * _discountMultiplier;
        }

        public override string GetDescription()
        {
            if (string.IsNullOrWhiteSpace(ConsultationTopic))
            {
                return "Консультація спеціаліста";
            }
            return $"Консультація спеціаліста: {ConsultationTopic}";
        }
    }
}
