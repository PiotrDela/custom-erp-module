using System.ComponentModel.DataAnnotations;

namespace CustomERP.Trucks.Api.Contracts
{
    public class CreateTruckRequestDto
    {
        [Required]
        public string Code { get; set; } // TODO: consider custom validation attribute that will check for alphanumeric string

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}