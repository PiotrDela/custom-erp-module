using System.ComponentModel.DataAnnotations;

namespace CustomERP.Api.Trucks
{
    public class CreateTruckRequest
    {
        [Required]
        // TODO: consider custom validation attribute that will check for alphanumeric string
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }        
    }
}