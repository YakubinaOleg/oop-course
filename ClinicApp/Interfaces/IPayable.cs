namespace ClinicApp.Interfaces
{
    public interface IPayable
    {
        void MarkPaid();
        bool IsPaid { get; }
        decimal GetCost();
    }
}
