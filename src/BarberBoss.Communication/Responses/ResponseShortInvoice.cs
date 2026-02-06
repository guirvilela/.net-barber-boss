using BarberBoss.Communication.Enums.InvoiceStatus;
using BarberBoss.Communication.Enums.PaymentMethod;

namespace BarberBoss.Communication.Responses;
public class ResponseShortInvoice
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public string BarberName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public InvoiceStatus Status { get; set; }
    public string Notes { get; set; } = string.Empty;

}
