using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public class LabResult : MedicalRecord
    {
        private string _testName = "";
        private string _unit = "";
        private string _referanceRange = "";

        public double Value {  get; set; }
        public bool IsNormal { get; set; }

        public string TestName
        {
            get => _testName;
            set
            {
                ClinicValidator.ValidateName(value, nameof(TestName));
                _testName = value;
            }
        }

        public string Unit
        {
            get => _unit;
            set
            {
                ClinicValidator.ValidateName(value, nameof(Unit));
                _unit = value;
            }
        }

        public string ReferenceRange
        {
            get => _referanceRange;
            set
            {
                ClinicValidator.ValidateName(value, nameof(ReferenceRange));
                _referanceRange = value;
            }
        }

        public LabResult(int patientId, int doctorId, DateTime date, string testName, double value, string unit, string referenceRange, bool isNormal)
        : base(patientId, doctorId, date)
        {
            TestName = testName;
            Value = value;
            Unit = unit;
            ReferenceRange = referenceRange;
            IsNormal = isNormal;
        }

        public override string GetSummary()
        {
            string text = $"{TestName}: {Value} {Unit} (норма: {ReferenceRange})";
            if (IsNormal == false)
            {
                text += " ⚠ поза нормою";
            }
            return text;
        }

        public override string GetRecordType()
         => "Аналіз";
    }
}
