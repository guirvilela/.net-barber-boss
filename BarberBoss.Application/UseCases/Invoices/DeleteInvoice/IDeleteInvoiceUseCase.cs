using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoices.DeleteInvoice;
public interface IDeleteInvoiceUseCase
{
    public Task<ResponseDeleteInvoiceJson> Execute(long id);
}
