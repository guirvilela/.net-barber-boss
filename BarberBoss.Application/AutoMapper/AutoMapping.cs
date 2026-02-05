using AutoMapper;
using BarberBoss.Communication.Enums.InvoiceStatus;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Communication.Responses.ResponseCreateInvoice;
using BarberBoss.Communication.Responses.ResponseGetAllInvoices;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Filters;

namespace BarberBoss.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }


    private void RequestToEntity()
    {
        CreateMap<RequestInvoiceJson, Invoice>()
         .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
         .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
         .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => InvoiceStatus.Paid));
 
        CreateMap<RequestGetInvoicesFilterJson, InvoiceFilter>();

    }

    private void EntityToResponse()
    {
        CreateMap<Invoice, ResponseCreateInvoiceJson>();
        CreateMap<Invoice, ResponseShortInvoice>();
        CreateMap<Invoice, ResponseInvoiceJson>();
    }

}
