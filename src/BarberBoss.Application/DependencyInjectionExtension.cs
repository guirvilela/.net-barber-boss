using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.UseCases.Invoices.CreateInvoice;
using BarberBoss.Application.UseCases.Invoices.DeleteInvoice;
using BarberBoss.Application.UseCases.Invoices.GetAllInvoices;
using BarberBoss.Application.UseCases.Invoices.GetInvoiceById;
using BarberBoss.Application.UseCases.Invoices.Reports.Excel;
using BarberBoss.Application.UseCases.Invoices.Reports.Pdf;
using BarberBoss.Application.UseCases.Invoices.UpdateInvoice;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ICreateInvoiceUseCase, CreateInvoiceUseCase>();
        services.AddScoped<IGetAllInvoicesUseCase, GetAllInvoicesUseCase>();
        services.AddScoped<IGetInvoiceByIdUseCase, GetInvoiceByIdUseCase>();
        services.AddScoped<IDeleteInvoiceUseCase, DeleteInvoiceUseCase>();
        services.AddScoped<IUpdateInvoiceUseCase, UpdateInvoiceUseCase>();

        services.AddScoped<IReportInvoicesToExcelUseCase, ReportInvoicesToExcelUseCase>();
        services.AddScoped<IReportInvoicesToPdfUseCase, ReportInvoicesToPdfUseCase>();
    }
}
