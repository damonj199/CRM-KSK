using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public DateOnly Date {  get; set; }
    public decimal Summa { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
    public Client Client { get; set; }
    public Membership Membership { get; set; }
}
