using GI.Application.DataTransferObjects.Client;
using MediatR;

namespace GI.Application.Features.Clients.Queries.GetClientSelectOption
{
    public class GetClientSelectOptionQuery : IRequest<IList<ClientSelectOptionDto>>
    {
        public int UserId { get; set; }
    }
}
