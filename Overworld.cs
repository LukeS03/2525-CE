using Godot;
using System;

public partial class Overworld : Node
{
	public const int MapSize = 512;
	public const double TerrainCap = 0.1;

	public TileMap OverworldTileMap;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OverworldTileMap = GetNode<TileMap>("OverworldTileMap");
		GenerateContinents();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	private void GenerateContinents()
	{
		var tileNoise = new FastNoiseLite();
		tileNoise.NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin;
		tileNoise.Seed = (int)new RandomNumberGenerator().Randi();

		for (var x = 0; x < MapSize; x++)
		{
			for (var y = 0; y < MapSize; y++)
			{
				var thisTileNoise = tileNoise.GetNoise2D(x, y);
				if (thisTileNoise > TerrainCap)
				{
					OverworldTileMap.SetCell(0, new Vector2I(x, y), 0, new Vector2I(0, 0), 0);
				}
				OverworldTileMap.SetCell(1, new Vector2I(x, y), 0, new Vector2I(0, 1), 0);
			}
		}
		
	}
}
