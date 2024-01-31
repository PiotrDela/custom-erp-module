using CustomERP.Domain.Trucks;
using System.ComponentModel.DataAnnotations;

namespace CustomERP.Trucks.Api.Contracts
{
    public class UpdateTruckRequestDto
    {
        [Required]
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public TrackUsageStatus? UsageStatus { get; set; }
    }
}