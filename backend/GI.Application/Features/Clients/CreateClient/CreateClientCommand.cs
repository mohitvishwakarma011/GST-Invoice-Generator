using GI.Application.DataTransferObjects.Client;
using MediatR;

namespace GI.Application.Features.Clients.CreateClient
{
    public class CreateClientCommand : IRequest<ClientDto>
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Gstin { get; set; }
        public string Email { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public string ShippingAddress {  get; set; } = string.Empty;
        public int StateCode { get; set; }
    }
}
