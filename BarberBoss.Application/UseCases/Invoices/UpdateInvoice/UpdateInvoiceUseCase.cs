using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using Microsoft.Extensions.Options;

namespace BarberBoss.Application.UseCases.Invoices.UpdateInvoice;
public class UpdateInvoiceUseCase : IUpdateInvoiceUseCase
{
    private IInvoiceUpdateOnly _repository;
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public UpdateInvoiceUseCase(IInvoiceUpdateOnly repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }



    public async Task Execute(long id, RequestInvoiceJson request)
    {
        Validate(request);

        var invoice = await _repository.GetInvoiceById(id);

        if(invoice == null)
        {
            throw new NotFoundException(ResourceErrors.INVOICE_NOT_FOUND);
        }

        _mapper.Map(request, invoice);

        _repository.UpdateInvoice(invoice);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestInvoiceJson request)
    {
        var validator = new InvoicesValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
