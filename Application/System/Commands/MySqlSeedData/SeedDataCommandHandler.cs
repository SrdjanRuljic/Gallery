using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Commands.MySqlSeedData
{
    public class SeedDataCommandHandler : IRequestHandler<SeedDataCommand>
    {
        private readonly IGalleryMySqlDbContext _context;
        private readonly IBlogService _blogService;

        public SeedDataCommandHandler(IGalleryMySqlDbContext context,
                                      IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }

        public async Task<Unit> Handle(SeedDataCommand request, CancellationToken cancellationToken)
        {
            DataSeeder seeder = new DataSeeder(_context, _blogService);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
