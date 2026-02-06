using BarberBoss.Domain.Filters;

namespace BarberBoss.Application.UseCases.Invoices.Reports.Excel;
public interface IReportInvoicesToExcelUseCase
{
    public Task<byte[]> Execute(DateOnly request);
}
