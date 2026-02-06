using BarberBoss.Domain.Enums.PaymentMethod;
using BarberBoss.Domain.Enums.InvoiceStatus;

namespace BarberBoss.Domain.Entities;
public class Invoice
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public string BarberName { get; set; } = String.Empty;
    public string ClientName { get; set; } = String.Empty;
    public string ServiceName { get; set; } = String.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public InvoiceStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
