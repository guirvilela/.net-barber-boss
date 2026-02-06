using BarberBoss.Domain.Filters;

namespace BarberBoss.Application.UseCases.Invoices.Reports.Pdf;
public interface IReportInvoicesToPdfUseCase
{
    public Task<byte[]> Execute(DateOnly request);
}
