using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Filters;
using BarberBoss.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BarberBoss.Infrastructure.DataAcess.Repositories;
public class BarberBossRepository : IInvoiceReadOnly, IInvoiceWriteOnly, IInvoiceUpdateOnly
{

    private readonly BarberBossDbContext _dbContext;

    public BarberBossRepository(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Invoice>> GetAllInvoices(InvoiceFilter? filters)
    {
        var query = _dbContext.Invoices.AsQueryable();

        if (filters != null) {
            if (filters.Date != null)
            {
                var date = filters.Date.Value;
                var startDate = date.Date;
                var endDate = date.Date.AddDays(1).AddTicks(-1);

                query = query
                    .Where(invoice => invoice.Date >= startDate && invoice.Date <= endDate)
                    .OrderBy(invoice => invoice.Date)
                    .ThenBy(invoice => invoice.ClientName);
            }
            if (!string.IsNullOrEmpty(filters.BarberName))
            {
                query = query.Where(invoice => invoice.BarberName == filters.BarberName);
            }
            if (!string.IsNullOrEmpty(filters.ClientName))
            {
                query = query.Where(invoice => invoice.ClientName == filters.ClientName);
            }
            if (filters.PaymentMethod != null)
            {
                query = query.Where(invoice => invoice.PaymentMethod == filters.PaymentMethod);
            }
            if (filters.Status != null)
            {
                query = query.Where(invoice => invoice.Status == filters.Status);
            }
           
        }

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<Invoice?> GetInvoiceById(long id)
    {
        return await _dbContext.Invoices.AsNoTracking().FirstOrDefaultAsync(invoice => invoice.Id == id);
    }

    public async Task CreateInvoice(Invoice invoice)
    {
        await _dbContext.AddAsync(invoice);
    }

    public async Task<long> DeleteInvoice(long id)
    {
        var invoice = await _dbContext.Invoices.FindAsync(id);

        if (invoice == null)
            return -1;

        _dbContext.Invoices.Remove(invoice);

        return id;
    }

    public void UpdateInvoice(Invoice invoice)
    {
        _dbContext.Update(invoice);
    }

    public async Task<List<Invoice>> FilterByMonth(DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59).Date;

        return await _dbContext.Invoices
            .AsNoTracking()
            .Where(invoice => invoice.Date >= startDate && invoice.Date <= endDate)
            .OrderBy(invoice => invoice.Date)
            .ThenBy(invoice => invoice.ClientName)
            .ToListAsync();
    }
}
