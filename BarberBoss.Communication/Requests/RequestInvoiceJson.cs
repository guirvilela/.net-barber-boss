using BarberBoss.Communication.Enums.InvoiceStatus;
using BarberBoss.Communication.Enums.PaymentMethod;

namespace BarberBoss.Communication.Requests;
public class RequestInvoiceJson
{
    public DateTime Date { get; set; }
    public string BarberName { get; set; } = String.Empty;
    public string ClientName { get; set; } = String.Empty;
    public string ServiceName { get; set; } = String.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public InvoiceStatus Status { get; set; }
    public string Notes { get; set; } = String.Empty;
}
