using BarberBoss.Communication.Enums.InvoiceStatus;
using BarberBoss.Communication.Enums.PaymentMethod;
using BarberBoss.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;
public class RequestInvoiceJsonBuilder
{
    public static RequestInvoiceJson Build()
    {
        return new Faker<RequestInvoiceJson>()
            .RuleFor(r => r.BarberName, faker => faker.Name.FullName())
            .RuleFor(r => r.ClientName, faker => faker.Name.FullName())
            .RuleFor(r => r.Status, faker => faker.PickRandom<InvoiceStatus>())
            .RuleFor(r => r.ServiceName, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentMethod, faker => faker.PickRandom<PaymentMethod>())
            .RuleFor(r => r.Notes, faker => faker.Lorem.Sentence());
    }
}
