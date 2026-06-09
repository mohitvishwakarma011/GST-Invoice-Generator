using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetInvoicesForUser
{
    public class GetInvoicesForUserQuery : BasePaginationQuery,IRequest<IList<InvoiceListDto>>
    {
        public int UserId { get; set; }
    }
}
