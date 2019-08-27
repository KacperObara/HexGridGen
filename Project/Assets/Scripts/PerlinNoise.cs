using UnityEngine;

namespace HexGen
{
    public class PerlinNoise : MonoBehaviour
    {
        public NoiseSettings settings;

        public HexGrid grid;

        public void Validate()
        {
            if (settings.lacunarity < 1)
                settings.lacunarity = 1;

            if (settings.octaves < 1)
                settings.octaves = 1;
        }

        public void GenerateNoiseMap()
        {
            Validate();

            grid.HeightMap = new float[grid.WorldWidth, grid.WorldHeight];
            grid.ColorMap = new Color[grid.WorldWidth, grid.WorldHeight];

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

                    for (int i = 0; i < settings.octaves; ++i)
                    {
                        float xCoord = x / settings.scale * frequency + offset * frequency;
                        float yCoord = y / settings.scale * frequency + offset * frequency;

                        float perlinValue = Mathf.PerlinNoise(xCoord, yCoord) * 2 - 1;

                        noiseHeight += perlinValue * amplitude;

                        amplitude *= settings.persistance;
                        frequency *= settings.lacunarity;
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
                    for (int i = 0; i < settings.Colors.Length; ++i)
                    {
                        if (currentHeight <= settings.Colors[i].Height)
                        {
                            grid.ColorMap[x, y] = settings.Colors[i].Color;

                            break;
                        }
                    }
                }
            }
        }
    }
}
