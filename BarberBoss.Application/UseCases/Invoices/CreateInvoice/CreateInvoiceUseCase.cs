using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Enums.InvoiceStatus;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception.ExceptionBase;
using BarberBoss.Communication.Responses.ResponseCreateInvoice;
using AutoMapper;


namespace BarberBoss.Application.UseCases.Invoices.CreateInvoice;
public class CreateInvoiceUseCase : ICreateInvoiceUseCase
{
    private readonly IInvoiceWriteOnly _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInvoiceUseCase(IInvoiceWriteOnly repository, IMapper mapper,  IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<ResponseCreateInvoiceJson> Execute(RequestInvoiceJson request)
    {
        Validate(request);

        var invoice = _mapper.Map<Invoice>(request);


        await _repository.CreateInvoice(invoice);

        await _unitOfWork.Commit();

        return new ResponseCreateInvoiceJson
        {
            Id = invoice.Id
        };
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
