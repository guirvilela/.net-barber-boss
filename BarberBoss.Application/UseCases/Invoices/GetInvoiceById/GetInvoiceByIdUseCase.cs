using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Invoices.GetInvoiceById;
public class GetInvoiceByIdUseCase : IGetInvoiceByIdUseCase
{
    private readonly IInvoiceReadOnly _repository;
    private readonly IMapper _mapper;

    public GetInvoiceByIdUseCase(IInvoiceReadOnly repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseInvoiceJson> Execute(long id)
    {
        var result = await _repository.GetInvoiceById(id);

        if (result is null)
        {
            throw new NotFoundException(ResourceErrors.INVOICE_NOT_FOUND);
        }

        return _mapper.Map<ResponseInvoiceJson>(result);

    }
}
