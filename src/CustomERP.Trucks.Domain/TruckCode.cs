namespace CustomERP.Trucks.Domain;

public class TruckCode : IEquatable<TruckCode>, IComparable<TruckCode>
{
    public static TruckCode Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new FormatException("Track code cannot be null nor empty");
        }

        if (code.All(char.IsLetterOrDigit) == false)
        {
            throw new FormatException("Track code should contain only letters and digits");
        }

        if (code.Any(char.IsDigit) == false || code.Any(char.IsLetter) == false)
        {
            throw new FormatException("Track code should contain both letters and digits");
        }

        return new TruckCode(code);
    }

    public string Value { get; }

    private TruckCode(string value)
    {
        Value = value.ToUpper();
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(TruckCode other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object obj)
    {
        return obj is TruckCode truckCode && Equals(truckCode);
    }

    public int CompareTo(TruckCode other)
    {
        return this.Value.CompareTo(other.Value);
    }
}
