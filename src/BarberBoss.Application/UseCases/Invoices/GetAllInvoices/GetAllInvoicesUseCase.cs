using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Communication.Responses.ResponseGetAllInvoices;
using BarberBoss.Domain.Filters;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.UseCases.Invoices.GetAllInvoices;
public class GetAllInvoicesUseCase : IGetAllInvoicesUseCase
{
    private readonly IInvoiceReadOnly _invoiceRepository;
    private readonly IMapper _mapper;

    public GetAllInvoicesUseCase(IInvoiceReadOnly invoiceRepository, IMapper mapper)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
    }

    public async Task<ResponseGetAllInvoicesJson> Execute(RequestGetInvoicesFilterJson? request)
    {
        var filters = request is null
        ? null
        : _mapper.Map<InvoiceFilter>(request);

        var result = await _invoiceRepository.GetAllInvoices(filters);

        return new ResponseGetAllInvoicesJson
        {
            Invoices = _mapper.Map<List<ResponseShortInvoice>>(result)
        };
    }



}
