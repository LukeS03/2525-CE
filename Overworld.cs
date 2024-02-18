using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using ce;

public partial class Overworld : Node
{
	public const int MapSize = 1024;
	public const double TerrainCap = 0.15;
	public Tile[,] mapTiles;

	public TileMap OverworldTileMap;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OverworldTileMap = GetNode<TileMap>("OverworldTileMap");
		mapTiles = new Tile[MapSize,MapSize];
		GenerateContinents();
		GenerateBeach();
		GenerateTileMap();

		//var UserPlayer = GD.Load<PackedScene>("res://Player.tscn");
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
				if (thisTileNoise > TerrainCap) mapTiles[x,y] = new Tile((TileType.GRASS), new Vector2I(x, y));
				else mapTiles[x,y] = new Tile((TileType.WATER), new Vector2I(x, y));
			}
		}
		
	}

	public void GenerateBeach()
	{
		foreach (Tile t in mapTiles)
		{
			Boolean setSand = false;
			if (t.type == TileType.GRASS)
			{
				for (int x = t.coords.X - 1; x <= t.coords.X + 1; x++)
				{
					for (int y = t.coords.Y - 1; y <= t.coords.Y + 1; y++)
					{
						if ((x >= 0) && (y >= 0) && (x < MapSize) && (y < MapSize))
						{
							if (mapTiles[x,y].type == TileType.WATER) {setSand = true;}
						}
					}
				}
			}
			if (setSand == true) t.type = TileType.BEACH;
		}
	}

	public void GenerateTileMap()
	{
		foreach(var t in mapTiles)
		{
			Vector2I canvasVector = new Vector2I(5, 5);
			switch (t.type)
			{
				case TileType.WATER:
					canvasVector = new Vector2I(0, 1);
					break;
				case TileType.GRASS:
					canvasVector = new Vector2I(1, 0);
					break;
				case TileType.BEACH:
					canvasVector = new Vector2I(0, 0);
					break;
			}
			OverworldTileMap.SetCell(0,t.coords,0,canvasVector,0);
		}
	}
}
