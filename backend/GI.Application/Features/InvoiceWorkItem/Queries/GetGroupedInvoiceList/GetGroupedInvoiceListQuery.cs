using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetGroupedInvoiceList
{
    public class GetGroupedInvoiceListQuery : IRequest<IList<GroupedInvoiceItemDto>>
    {
        public int UserId { get; set; }
    }
}
