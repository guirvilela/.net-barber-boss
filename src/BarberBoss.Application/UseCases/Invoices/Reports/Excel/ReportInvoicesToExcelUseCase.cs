using BarberBoss.Domain.Enums.PaymentMethod.Extensions;
using BarberBoss.Domain.Filters;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BarberBoss.Application.UseCases.Invoices.Reports.Excel;
public class ReportInvoicesToExcelUseCase : IReportInvoicesToExcelUseCase
{
    private readonly IInvoiceReadOnly _repository;
    private string CURRENCY_SYMBOL = ResourceReportInvoiceGenerationMessage.CURRENCY_SYMBOL;

    public ReportInvoicesToExcelUseCase(IInvoiceReadOnly repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var invoices = await _repository.FilterByMonth(month);

        if(invoices.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook();

        workbook.Author = "Guilherme Vilela";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var monthpage = month.ToString("Y");

        var worksheet = workbook.Worksheets.Add(monthpage);

        InsertHeader(worksheet);

        var raw = 2;

        foreach(var invoice in invoices)
        {
            worksheet.Cell($"A{raw}").Value = invoice.ServiceName;
            worksheet.Cell($"B{raw}").Value = invoice.Date;
            worksheet.Cell($"C{raw}").Value = invoice.PaymentMethod.PaymentsMethodToString();

            worksheet.Cell($"D{raw}").Value = invoice.Amount;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"-{ResourceReportInvoiceGenerationMessage.CURRENCY_SYMBOL} #,##0.00";

            worksheet.Cell($"E{raw}").Value = invoice.Notes;

            raw++;

        }

        worksheet.Columns("A:H").AdjustToContents();

        var file = new MemoryStream();

        workbook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportInvoiceGenerationMessage.TITLE;
        worksheet.Cell("B1").Value = ResourceReportInvoiceGenerationMessage.DATE;
        worksheet.Cell("C1").Value = ResourceReportInvoiceGenerationMessage.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportInvoiceGenerationMessage.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportInvoiceGenerationMessage.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }   
}       
        
        