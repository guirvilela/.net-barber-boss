using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories;
public interface IInvoiceUpdateOnly
{
    /// <summary>
    /// This function return Invoice by Id to update an Invoice correctly
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Invoice?> GetInvoiceById(long id);

    /// <summary>
    /// This function update an existing Invoice
    /// </summary>
    /// <param name="invoice"></param>
    void UpdateInvoice(Invoice invoice);
}
