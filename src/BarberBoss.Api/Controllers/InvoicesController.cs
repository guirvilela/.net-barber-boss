using BarberBoss.Application.UseCases.Invoices.CreateInvoice;
using BarberBoss.Application.UseCases.Invoices.DeleteInvoice;
using BarberBoss.Application.UseCases.Invoices.GetAllInvoices;
using BarberBoss.Application.UseCases.Invoices.GetInvoiceById;
using BarberBoss.Application.UseCases.Invoices.UpdateInvoice;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Communication.Responses.ResponseCreateInvoice;
using BarberBoss.Communication.Responses.ResponseGetAllInvoices;
using BarberBoss.Domain.Entities;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseGetAllInvoicesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllInvoices([FromServices] IGetAllInvoicesUseCase useCase, [FromQuery] RequestGetInvoicesFilterJson? filters)
    {
        var response = await useCase.Execute(filters);

        if (response.Invoices.Count != 0) return Ok(response);

        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseInvoiceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInvoiceById([FromServices] IGetInvoiceByIdUseCase useCase, [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreateInvoiceJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateInvoice([FromServices] ICreateInvoiceUseCase useCase, [FromBody] RequestInvoiceJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);

    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateInvoice([FromServices] IUpdateInvoiceUseCase useCase, [FromBody] RequestInvoiceJson request, [FromRoute] long id)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseDeleteInvoiceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleleInvoice([FromServices] IDeleteInvoiceUseCase useCase, [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }
}
