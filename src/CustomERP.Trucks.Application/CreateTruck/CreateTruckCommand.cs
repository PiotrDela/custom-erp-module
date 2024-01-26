namespace CustomERP.Trucks.Application.CreateTruck
{
    public class CreateTruckCommand : ICommand<Guid>
    {
        public string Code { get; }
        public string Name { get; }
        public string Description { get; }

        public CreateTruckCommand(string code, string name, string description)
        {
            Code = code;
            Name = name;
            Description = description;
        }
    }
}
