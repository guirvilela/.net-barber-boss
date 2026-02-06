using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses.ResponseGetAllInvoices;

namespace BarberBoss.Application.UseCases.Invoices.GetAllInvoices;
public interface IGetAllInvoicesUseCase
{
    public Task<ResponseGetAllInvoicesJson> Execute(RequestGetInvoicesFilterJson? filters);
}
