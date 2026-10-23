using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Managers
{
    public class BillingManager
    {
        private readonly AppointmentManager _appointmentManager; 
        public BillingManager(AppointmentManager ap)
        {
            _appointmentManager = ap;
        }

        private IPayable[] FilterUnpaid(Appointment[] source)
        {
            int count = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (!source[i].IsPaid && !source[i].IsCancelled)
                    count++;
            }

            IPayable[] result = new IPayable[count];
            int index = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (!source[i].IsPaid && !source[i].IsCancelled)
                {
                    result[index] = source[i];
                    index++;
                }
            }
            return result;
        }

        // 1.all unpaid
        public IPayable[] GetAllUnpaid()
        {
            return FilterUnpaid(_appointmentManager.GetAll());
        }

        public IPayable[] GetUnpaidByPatient(int patientId)
        {
            Appointment[] all = _appointmentManager.GetAll();

            int count = 0;
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].PatientId == patientId)
                    count++;
            }
            Appointment[] byPatient = new Appointment[count];
            int index = 0;
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].PatientId == patientId)
                {
                    byPatient[index] = all[i];
                    index++;
                }
            }

            return FilterUnpaid(byPatient);
        }
        //3.total debt
        public decimal GetTotalDebt()
        {
            var unpaidItems = GetAllUnpaid();
            decimal totalDebt = 0m;

            for (int i = 0; i < unpaidItems.Length; i++)
            {
                totalDebt += unpaidItems[i].GetCost();
            }
            return totalDebt;
        }
        //4.patient debt
        public decimal GetPatientDebt(int patientId)
        {
            var unpaidItems = GetUnpaidByPatient(patientId);
            decimal patientDebt = 0m;

            for (int i = 0; i < unpaidItems.Length; i++)
            {
                patientDebt += unpaidItems[i].GetCost();
            }
            return patientDebt;
        }

        public bool PayAppointment(int appointmentId)
        {
            Appointment[] all = _appointmentManager.GetAll();

            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].Id == appointmentId)
                {
                    if (all[i].IsPaid || all[i].IsCancelled)
                        return false;

                    all[i].MarkPaid();
                    return true;
                }
            }
            return false; 
        }

        public void DisplayUnpaid(IPayable[] items)
        {
            if (items.Length == 0)
            {
                Console.WriteLine("Немає неоплачених записів.");
                return;
            }

            Console.WriteLine($"=== Неоплачені записи ({items.Length}) ===");
            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {items[i]} | Сума: {items[i].GetCost():F2} грн");
            }
        }
    }
}
