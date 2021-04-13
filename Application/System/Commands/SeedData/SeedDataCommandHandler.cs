using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData
{
    class SeedDataCommandHandler : IRequestHandler<SeedDataCommand>
    {
        private readonly IGalleryDbContext _context;

        public SeedDataCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SeedDataCommand request, CancellationToken cancellationToken)
        {
            DataSeeder seeder = new DataSeeder(_context);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
