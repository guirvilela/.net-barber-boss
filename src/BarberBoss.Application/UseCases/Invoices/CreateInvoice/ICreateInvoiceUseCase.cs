using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses.ResponseCreateInvoice;

namespace BarberBoss.Application.UseCases.Invoices.CreateInvoice;
public interface ICreateInvoiceUseCase
{
    Task<ResponseCreateInvoiceJson> Execute(RequestInvoiceJson request);
}
