using Application.Common.Interfaces;
using MediatR;
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
        private readonly IManagersServices _managersServices;

        public SeedDataCommandHandler(IGalleryDbContext context,
                                      IManagersServices managersServices)
        {
            _context = context;
            _managersServices = managersServices;
        }

        public async Task<Unit> Handle(SeedDataCommand request, CancellationToken cancellationToken)
        {
            DataSeeder seeder = new DataSeeder(_context, _managersServices);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
