using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Filters;

namespace BarberBoss.Domain.Repositories;
public interface IInvoiceReadOnly
{
    /// <summary>
    /// This function return all Invoices registered on Database
    /// </summary>
    /// <returns></returns>
    Task<List<Invoice>> GetAllInvoices(InvoiceFilter? filters);

    /// <summary>
    /// This function return Invoice by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Invoice?> GetInvoiceById(long id);



    Task<List<Invoice>> FilterByMonth(DateOnly date);

}
