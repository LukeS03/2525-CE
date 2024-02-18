using Godot;

namespace ce;

public class Tile
{
    public TileType type;
    public Vector2I coords;
    
    public Tile(TileType type, Vector2I coords) 
    {
        this.type  = type;
        this.coords = coords;
    }
}

public enum TileType
{
    WATER,
    BEACH,
    GRASS,
    ARABLELAND,
    ROCK
}

