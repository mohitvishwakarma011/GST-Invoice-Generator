using GI.Application.DataTransferObjects.User;
using MediatR;

namespace GI.Application.Features.UserItem
{
    public class GetUserInfoQuery : IRequest<UserDetailDto>
    {
        public int UserId { get; set; }
    }
}
