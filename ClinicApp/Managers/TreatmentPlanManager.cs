using ClinicApp.Enums;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp.Managers
{
    public class TreatmentPlanManager
    {
        private readonly List<TreatmentPlan> _plans = new List<TreatmentPlan>();

        public bool Add(TreatmentPlan plan)
        {
            ValidationResult result = ModelValidator.Validate(plan);
            if (!result.IsValid)
            {
                result.Print();
                return false;
            }

            _plans.Add(plan);
            return true;
        }

        public TreatmentPlan? GetById(int id)
        {
            for (int i = 0; i < _plans.Count; i++)
            {
                if (_plans[i].Id == id)
                    return _plans[i];
            }
            return null;
        }

        public TreatmentPlan[] GetByPatient(int patientId)
        {
            List<TreatmentPlan> found = new List<TreatmentPlan>();
            for (int i = 0; i < _plans.Count; i++)
            {
                if (_plans[i].PatientId == patientId)
                    found.Add(_plans[i]);
            }
            return found.ToArray();
        }

        public TreatmentPlan[] GetByStatus(TreatmentStatus status)
        {
            List<TreatmentPlan> found = new List<TreatmentPlan>();
            for (int i = 0; i < _plans.Count; i++)
            {
                if (_plans[i].Status == status)
                    found.Add(_plans[i]);
            }
            return found.ToArray();
        }

        public TreatmentPlan[] GetAll()
        {
            return _plans.ToArray();
        }
    }
}