

using BarberBoss.Communication.Enums.InvoiceStatus;
using BarberBoss.Communication.Enums.PaymentMethod;

namespace BarberBoss.Communication.Requests;
public class RequestGetInvoicesFilterJson
{
    public DateTime? Date { get; set; }
    public string? BarberName { get; set; }
    public string? ClientName { get; set; }
    public decimal? Amount { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public InvoiceStatus? Stauts { get; set; }
}
