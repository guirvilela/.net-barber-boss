

using BarberBoss.Domain.Enums.InvoiceStatus;
using BarberBoss.Domain.Enums.PaymentMethod;

namespace BarberBoss.Domain.Filters;
public class InvoiceFilter
{
    public DateTime? Date { get; set; }
    public string? BarberName { get; set; }
    public string? ClientName { get; set; }
    public decimal? Amount { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public InvoiceStatus? Status { get; set; }
}
