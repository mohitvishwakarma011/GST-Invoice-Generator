using GI.Application.Common.Interfaces;
using GI.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Seed
{
    public class SeedDbCommandHandler(IAppDbContext appDbContext) : IRequestHandler<SeedDbCommand>
    {
        public async Task Handle(SeedDbCommand request, CancellationToken cancellationToken)
        {
            var availableCodes = (await appDbContext.States.ToListAsync()).Select(x => x.Code);
            var toBePopulated = GetStates().Where(x => !availableCodes.Contains(x.Code));
            appDbContext.States.AddRange(toBePopulated);
            await appDbContext.SaveChangesAsync();
        }

        private IList<State> GetStates()
        {
            return new List<State>
            {
                new State { Name = "Jammu and Kashmir", Code = 1, Description = string.Empty },
                new State { Name = "Himachal Pradesh", Code = 2, Description = string.Empty },
                new State { Name = "Punjab", Code = 3, Description = string.Empty },
                new State { Name = "Chandigarh", Code = 4, Description = string.Empty },
                new State { Name = "Uttarakhand", Code = 5, Description = string.Empty },
                new State { Name = "Haryana", Code = 6, Description = string.Empty },
                new State { Name = "Delhi", Code = 7, Description = string.Empty },
                new State { Name = "Rajasthan", Code = 8, Description = string.Empty },
                new State { Name = "Uttar Pradesh", Code = 9, Description = string.Empty },
                new State { Name = "Bihar", Code = 10, Description = string.Empty },
                new State { Name = "Sikkim", Code = 11, Description = string.Empty },
                new State { Name = "Arunachal Pradesh", Code = 12, Description = string.Empty },
                new State { Name = "Nagaland", Code = 13, Description = string.Empty },
                new State { Name = "Manipur", Code = 14, Description = string.Empty },
                new State { Name = "Mizoram", Code = 15, Description = string.Empty },
                new State { Name = "Tripura", Code = 16, Description = string.Empty },
                new State { Name = "Meghalaya", Code = 17, Description = string.Empty },
                new State { Name = "Assam", Code = 18, Description = string.Empty },
                new State { Name = "West Bengal", Code = 19, Description = string.Empty },
                new State { Name = "Jharkhand", Code = 20, Description = string.Empty },
                new State { Name = "Odisha", Code = 21, Description = string.Empty },
                new State { Name = "Chhattisgarh", Code = 22, Description = string.Empty },
                new State { Name = "Madhya Pradesh", Code = 23, Description = string.Empty },
                new State { Name = "Gujarat", Code = 24, Description = string.Empty },
                new State { Name = "Daman and Diu", Code = 25, Description = string.Empty },
                new State { Name = "Dadra and Nagar Haveli", Code = 26, Description = string.Empty },
                new State { Name = "Maharashtra", Code = 27, Description = string.Empty },
                new State { Name = "Andhra Pradesh", Code = 28, Description = string.Empty },
                new State { Name = "Karnataka", Code = 29, Description = string.Empty },
                new State { Name = "Goa", Code = 30, Description = string.Empty },
                new State { Name = "Lakshadweep", Code = 31, Description = string.Empty },
                new State { Name = "Kerala", Code = 32, Description = string.Empty },
                new State { Name = "Tamil Nadu", Code = 33, Description = string.Empty },
                new State { Name = "Puducherry", Code = 34, Description = string.Empty },
                new State { Name = "Andaman and Nicobar Islands", Code = 35, Description = string.Empty },
                new State { Name = "Telangana", Code = 36, Description = string.Empty },
                new State { Name = "Andhra Pradesh (New)", Code = 37, Description = string.Empty }
            };
        }
    }
}
