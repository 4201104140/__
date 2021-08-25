
namespace data;

public class Cattle
{
    public string Id { get; set; }

    public string Name { get; set; }

    public DateTime Birthday { get; set; }

    public CattleType CattleType { get; set; }

    public Cattle(CattleType type)
    {
        Id = type.ToString().ToLower() + Guid.NewGuid().ToString();
    }
}

public enum CattleType
{
    Pig,
    Cow,
    Unknown
}