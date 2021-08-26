using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData
{
    public class SeedDataCommand : IRequest
    {
    }

    public class SeedDataCommandHandler : IRequestHandler<SeedDataCommand>
    {
        private readonly IGalleryDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedDataCommandHandler(IGalleryDbContext context,
                                      RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(SeedDataCommand request, CancellationToken cancellationToken)
        {
            DataSeeder seeder = new DataSeeder(_context, _roleManager);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
