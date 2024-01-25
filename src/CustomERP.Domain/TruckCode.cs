namespace CustomERP.Domain;

public class TruckCode
{
    public static TruckCode Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new FormatException("Track code cannot be null nor empty");
        }

        if (code.All(char.IsLetterOrDigit) == false)
        {
            throw new FormatException("Track code should contain only letters or digits");
        }

        return new TruckCode(code);
    }

    public string Value { get; }

    private TruckCode(string value)
    {
        this.Value = value;
    }
}
