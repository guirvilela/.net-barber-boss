using BarberBoss.Application.UseCases.Invoices;
using BarberBoss.Communication.Enums.PaymentMethod;
using BarberBoss.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Invoices.CreateInvoice;
public class InvoicesValidatorTests
{
    [Fact]
    public void SuccessToCreateInvoice()
    {
        //Arrange (Configurar a instancias para executar os testes)
        var validator = new InvoicesValidator();

        var request = RequestInvoiceJsonBuilder.Build();

        //Act (ação de validar o teste)

        var result = validator.Validate(request);

        //Assets (Resultado esperado)

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-20)]
    public void Error_Amount_Equal_Zero(decimal amount)
    {
        //Arrange
        var validator = new InvoicesValidator();
        var request = RequestInvoiceJsonBuilder.Build();

        request.Amount = amount;
        //Act
        var result = validator.Validate(request);

        //Assets
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrors.VALIDATION_AMOUNT_REQUIRED));
    }

    [Theory]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    public void Error_Barber_Name_Is_Empty(String name)
    {
        //Arrange
        var validator = new InvoicesValidator();
        var request = RequestInvoiceJsonBuilder.Build();

         request.BarberName = name;
        //Act
        var result = validator.Validate(request);
        //Assets
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrors.VALIDATION_BARBER_NAME_REQUIRED));

    }

    [Theory]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    public void Error_Client_Name_Is_Empty(String clientName)
    {
        //Arrange
        var validator = new InvoicesValidator();
        var request = RequestInvoiceJsonBuilder.Build();
        request.ClientName = clientName;
        //Act
         var result = validator.Validate(request);

        //Assets
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrors.VALIDATION_CLIENT_NAME_REQUIRED));
    }

    [Fact]
    public void Error_Service_Name_Is_Empty()
    {
        //Arrange
        var validator = new InvoicesValidator();
        var request = RequestInvoiceJsonBuilder.Build();
        request.ServiceName = "";
        //Act
        var result = validator.Validate(request);

        //Assets
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrors.VALIDATION_SERVICE_NAME_REQUIRED));
    }

    [Fact]
    public void Error_Payment_Method_Out_Of_Enum()
    {
        //Arrange
        var validator = new InvoicesValidator();
        var request = RequestInvoiceJsonBuilder.Build();

        request.PaymentMethod = (PaymentMethod)100;
        //Act
        var result = validator.Validate(request);
        //Assets
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrors.VALIDATION_PAYMENT_METHOD_REQUIRED));
    }


}
