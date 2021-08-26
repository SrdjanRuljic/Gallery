using Application.Common.Interfaces;
using Domain.Entities;
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
        private readonly UserManager<AppUser> _userManager;

        public SeedDataCommandHandler(IGalleryDbContext context,
                                      RoleManager<IdentityRole> roleManager,
                                      UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(SeedDataCommand request, CancellationToken cancellationToken)
        {
            DataSeeder seeder = new DataSeeder(_context, _roleManager, _userManager);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
