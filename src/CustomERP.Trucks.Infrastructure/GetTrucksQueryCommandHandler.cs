﻿using CustomERP.Trucks.Application;
using CustomERP.Trucks.Application.GetTrucks;

namespace CustomERP.Trucks.Infrastructure
{
    public class GetTrucksQueryCommandHandler : IQueryHandler<GetTrucksQuery, IEnumerable<TruckDto>>
    {
        private readonly TrucksDbContext dbContext;

        public GetTrucksQueryCommandHandler(TrucksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<IEnumerable<TruckDto>> Handle(GetTrucksQuery request, CancellationToken cancellationToken)
        {
            var trucks = dbContext.Trucks.AsQueryable();

            if (string.IsNullOrWhiteSpace(request.Parameters.Name) == false)
            {
                trucks = trucks.Where(x => x.Name.Contains(request.Parameters.Name));
            }

            var result = trucks.ToArray();

            return Task.FromResult(result.Select(TruckDto.Create));
        }
    }
}
