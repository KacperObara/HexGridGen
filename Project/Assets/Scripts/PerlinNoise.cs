using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu]
    public class PerlinNoise : NoiseGen
    {
        public NoiseSettings settings;

        public HexGrid grid;

        public void Validate()
        {
            if (settings.Lacunarity < 1)
            {
                settings.Lacunarity = 1;
                Debug.LogWarning("Lacunarity can't be < 1. Changing to 1");
            }

            if (settings.Octaves < 1)
            {
                settings.Octaves = 1;
                Debug.LogWarning("Octaves can't be < 1. Changing to 1");
            }
        }

        public override void Initialize(Generator generator)
        {
            this.grid = generator.HexGrid;
            this.settings = generator.NoiseSettings;
        }

        public override void Generate()
        {
            Validate();

            grid.HeightMap = new float[grid.WorldWidth, grid.WorldHeight];
            grid.ColorMap = new Color[grid.WorldWidth, grid.WorldHeight];

            if (settings.Seed != 0)
                Random.InitState(settings.Seed);
            float offset = Random.Range(0, 999999);

            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;

            for (int y = 0; y < grid.WorldHeight; ++y)
            {
                for (int x = 0; x < grid.WorldWidth; ++x)
                {
                    float amplitude = 1;
                    float frequency = 1;
                    float noiseHeight = 0;

                    for (int i = 0; i < settings.Octaves; ++i)
                    {
                        float xCoord = x / settings.Scale * frequency + offset * frequency;
                        float yCoord = y / settings.Scale * frequency + offset * frequency;

                        float perlinValue = Mathf.PerlinNoise(xCoord, yCoord) * 2 - 1;

                        noiseHeight += perlinValue * amplitude;

                        amplitude *= settings.Persistance;
                        frequency *= settings.Lacunarity;
                    }

                    if (noiseHeight > maxNoiseHeight)
                    {
                        maxNoiseHeight = noiseHeight;
                    }
                    if (noiseHeight < minNoiseHeight)
                    {
                        minNoiseHeight = noiseHeight;
                    }

                    grid.HeightMap[x, y] = noiseHeight;
                }
            }

            for (int y = 0; y < grid.WorldHeight; ++y)
            {
                for (int x = 0; x < grid.WorldWidth; ++x)
                {
                    grid.HeightMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, grid.HeightMap[x, y]);
                }
            }

            ApplyColors();
        }

        public void ApplyColors()
        {
            for (int y = 0; y < grid.WorldHeight; ++y)
            {
                for (int x = 0; x < grid.WorldWidth; ++x)
                {
                    float currentHeight = grid.HeightMap[x, y];
                    for (int i = 0; i < settings.TerrainTypes.Length; ++i)
                    {
                        if (currentHeight <= settings.TerrainTypes[i].NoiseHeight)
                        {
                            grid.ColorMap[x, y] = settings.TerrainTypes[i].Color;
                            grid.Hexes[x + y * grid.WorldWidth].TerrainType = settings.TerrainTypes[i];

                            break;
                        }
                    }
                }
            }
        }
    }
}
