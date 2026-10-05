using GI.Application.DataTransferObjects.State;
using MediatR;

namespace GI.Application.Features.StateItem.Query
{
    public class GetStateQuery : IRequest<IList<StateDto>>
    {
    }
}
