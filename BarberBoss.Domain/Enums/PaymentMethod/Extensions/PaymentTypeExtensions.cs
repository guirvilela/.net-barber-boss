using BarberBoss.Domain.Reports;
using System.Runtime.CompilerServices;

namespace BarberBoss.Domain.Enums.PaymentMethod.Extensions;
public static  class PaymentTypeExtensions
{
    public static string PaymentsMethodToString(this PaymentMethod paymentMethod)
    {
        return paymentMethod switch
        {
            PaymentMethod.Card => ResourceReportInvoiceGenerationMessage.CARD,
            PaymentMethod.Money => ResourceReportInvoiceGenerationMessage.CASH,
            PaymentMethod.Pix => ResourceReportInvoiceGenerationMessage.PIX,
            PaymentMethod.Other => ResourceReportInvoiceGenerationMessage.OTHER,
            _ => String.Empty
        };
    }
}
