using AutoMapper;
using GI.Application.DataTransferObjects.Client;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using GI.Core.Entities;

namespace GI.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Invoice,InvoiceDetailDto>().ReverseMap();
            CreateMap<InvoiceItem,InvoiceItemDto>().ReverseMap();
            CreateMap<Client,ClientDto>().ReverseMap();
        }
    }
}
