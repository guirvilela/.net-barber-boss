using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Invoices.DeleteInvoice;
public class DeleteInvoiceUseCase : IDeleteInvoiceUseCase
{
    private IInvoiceWriteOnly _repository;
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public DeleteInvoiceUseCase(IInvoiceWriteOnly repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseDeleteInvoiceJson> Execute(long id)
    {
        var result = await _repository.DeleteInvoice(id);
        
        if(result == -1)
        {
            throw new NotFoundException(ResourceErrors.INVOICE_NOT_FOUND);
        }

        await _unitOfWork.Commit();

        return new ResponseDeleteInvoiceJson { Id = result };
    }
}
