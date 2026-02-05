using BarberBoss.Communication.Requests;
using FluentValidation;
using BarberBoss.Exception;

namespace BarberBoss.Application.UseCases.Invoices;
public class InvoicesValidator : AbstractValidator<RequestInvoiceJson>
{

    public InvoicesValidator()
    {
        RuleFor(invoice => invoice.Amount).GreaterThan(0).WithMessage(ResourceErrors.VALIDATION_AMOUNT_REQUIRED);
        RuleFor(invoice => invoice.BarberName).NotEmpty().WithMessage(ResourceErrors.VALIDATION_BARBER_NAME_REQUIRED);
        RuleFor(invoice => invoice.ClientName).NotEmpty().WithMessage(ResourceErrors.VALIDATION_CLIENT_NAME_REQUIRED);
        RuleFor(invoice => invoice.ServiceName).NotEmpty().WithMessage(ResourceErrors.VALIDATION_SERVICE_NAME_REQUIRED);
        RuleFor(invoice => invoice.PaymentMethod).IsInEnum().WithMessage(ResourceErrors.VALIDATION_PAYMENT_METHOD_REQUIRED);
    }
}
