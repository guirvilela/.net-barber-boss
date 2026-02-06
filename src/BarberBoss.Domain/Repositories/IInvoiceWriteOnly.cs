using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories;
public interface IInvoiceWriteOnly
{
    /// <summary>
    /// This function create a new Invoice
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns></returns>
    Task CreateInvoice(Invoice invoice);


    /// <summary>
    ///  This function return Invoice ID if deleted successfully, otherwise return -1
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<long> DeleteInvoice(long id);
}
