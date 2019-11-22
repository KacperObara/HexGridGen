using HexGen;

public interface IEntity
{
    Hex OccupiedHex { get; set; }
}

public interface IMovable : IEntity
{
    int Range { get; }
    bool Moved { get; set; }
}