using GI.Application.DataTransferObjects.InvoiceWorkItem;
using GI.Application.Features.Global.Queries;
using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetInvoicesForUser
{
    public class GetInvoicesForUserQuery : BasePaginationQuery,IRequest<IList<InvoiceListDto>>
    {
        public int UserId { get; set; }
    }
}
