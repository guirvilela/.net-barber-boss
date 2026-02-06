using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.UseCases.Invoices.UpdateInvoice;
public interface IUpdateInvoiceUseCase
{
    public Task Execute(long id, RequestInvoiceJson request);
}
