using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu]
    public class DefaultPerlinNoiseGenerator : NoiseGenerator
    {
        private MapSettings settings;
        private MapData mapData;

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
            this.mapData = generator.MapData;
            this.settings = generator.MapSettings;
        }

        private float PickGenerationSeed()
        {
            if (settings.Seed != 0)
            {
                Random.InitState(settings.Seed);
                settings.ActualSeed = settings.Seed;
            }
            else
            {
                int seed = Random.Range(0, 999999);
                Random.InitState(seed);
                settings.ActualSeed = seed;
            }

            return Random.Range(0, 999999);
        }

        public override void Generate()
        {
            Validate();
            float offset = PickGenerationSeed();

            mapData.HeightMap = new float[settings.WorldWidth, settings.WorldHeight];
            mapData.TextureIndex = new int[settings.WorldWidth * settings.WorldHeight];

            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;

            for (int y = 0; y < settings.WorldHeight; ++y)
            {
                for (int x = 0; x < settings.WorldWidth; ++x)
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

                    mapData.HeightMap[x, y] = noiseHeight;
                }
            }

            for (int y = 0; y < settings.WorldHeight; ++y)
            {
                for (int x = 0; x < settings.WorldWidth; ++x)
                {
                    mapData.HeightMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, mapData.HeightMap[x, y]);
                }
            }

            ApplyColors();
        }

        public override void Clear()
        {
            
        }

        public void ApplyColors()
        {
            for (int y = 0; y < settings.WorldHeight; ++y)
            {
                for (int x = 0; x < settings.WorldWidth; ++x)
                {
                    float currentHeight = mapData.HeightMap[x, y];
                    for (int i = 0; i < settings.TerrainTypes.Length; ++i)
                    {
                        if (currentHeight <= settings.TerrainTypes[i].NoiseHeight)
                        {
                            mapData.TextureIndex[x + y * settings.WorldWidth] = i;

                            mapData.Hexes[x + y * settings.WorldWidth].TerrainType = settings.TerrainTypes[i];

                            break;
                        }
                    }
                }
            }
        }
    }
}
