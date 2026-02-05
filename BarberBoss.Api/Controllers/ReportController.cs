using BarberBoss.Application.UseCases.Invoices.Reports.Excel;
using BarberBoss.Application.UseCases.Invoices.Reports.Pdf;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BarberBoss.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromServices] IReportInvoicesToExcelUseCase useCase, [FromQuery] DateOnly month)
    {
        byte[] file = await useCase.Execute(month);

        if(file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
        }

        return NoContent();
    }

    [HttpGet("pdf")]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPdf([FromServices] IReportInvoicesToPdfUseCase useCase, [FromQuery] DateOnly month)
    {
        byte[] file = await useCase.Execute(month);

        if(file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");
        }

        return NoContent();
    }

}
