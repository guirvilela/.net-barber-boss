using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoices.GetInvoiceById;
public interface IGetInvoiceByIdUseCase
{
    public Task<ResponseInvoiceJson> Execute(long id);
}
