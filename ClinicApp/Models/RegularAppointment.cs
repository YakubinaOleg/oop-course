namespace ClinicApp.Models
{
    public class RegularAppointment : Appointment
    {
        private readonly decimal _discountMultiplier;
        public RegularAppointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30, decimal discountMultiplier = 1.0m)
            : base(patientId, doctorId, scheduledAt, durationMinutes)
        {
            _discountMultiplier = discountMultiplier;
        }

        public override string GetDescription()
        {
            return "Звичайний прийом";
        }

        public override decimal GetCost()
        {
            return base.GetCost() * _discountMultiplier;
        }
    }
    /*
 * ЗАДАЧА 5: Вибір підходу для VIP-знижки.
 * Обрано Варіант Б (коефіцієнт знижки в конструкторі через readonly-поле).
 * 
 * Чому так:
 * Щоб не плодити купу зайвих класів на кожну комбінацію (типу VipUrgentAppointment і т.д.), 
 * що призвело б до вибуху ієрархії. Передаємо множник знижки прямо в конструктор, 
 * тому об'єкт одразу створюється з потрібною ціною і її неможливо випадково змінити (readonly).
 */
}
