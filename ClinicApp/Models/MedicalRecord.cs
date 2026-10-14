using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public abstract class MedicalRecord
    {
        private static int _nextId = 1;

        public int Id { get; }
        public int PatientId { get; }
        public int DoctorId {  get; }
        public DateTime Date {  get; }
        public string Notes { get; set; }

        protected MedicalRecord(int pId, int dId, DateTime date) 
        {
            ClinicValidator.ValidatePositive(pId, nameof(PatientId));
            ClinicValidator.ValidatePositive(dId, nameof(DoctorId));
            Id = _nextId++;
            PatientId = pId;
            DoctorId = dId;
            Date = date;
            Notes = "";
        }

        public abstract string GetSummary();
        public virtual string GetRecordType() => "Медичний запис";
        public virtual bool IsActive()
        {
            if (Date >= DateTime.Today.AddMonths(-6)) {  return true; }
            else { return false; }
        }

        public override string ToString()
        {
            string result = $"[{Id}] {GetRecordType()} | {Date:dd.MM.yyyy} | {GetSummary()}";

            if (!string.IsNullOrEmpty(Notes))
            {
                result += $" | {Notes}";
            }

            return result;
        }
    }
}
