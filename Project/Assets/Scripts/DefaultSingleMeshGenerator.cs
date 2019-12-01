using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu]
    public class DefaultSingleMeshGenerator : MeshGenerator
    {
        private MapSettings mapSettings;
        private MapData mapData;
        private Mesh mesh;
        private MeshCollider meshCollider;
        private ShaderManager shaderManager;

        private List<Vector3> vertices;
        private List<int> triangles;

        private List<Vector2> uvs;

        public override void Initialize(Generator generator)
        {
            this.mapData = generator.MapData;
            this.mapSettings = generator.MapSettings;
            this.meshCollider = generator.GetComponent<MeshCollider>();
            this.shaderManager = generator.shaderManager;

            vertices = new List<Vector3>();
            triangles = new List<int>();
            uvs = new List<Vector2>();

            mesh = generator.GetComponent<MeshFilter>().mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // allows for meshes bigger than ~65000 vertices
        }

        /// <summary>
        /// Clear mesh, generate hexagons and assign them to mesh
        /// </summary>
        /// <param name="cells"></param>
        public override void Generate()
        {
            ClearMesh();

            foreach (Hex cell in mapData.Hexes)
            {
                //mapData.Hexes[cell.LocalPos.x + cell.LocalPos.y * mapSettings.WorldWidth].TerrainType = mapData.Hexes[cell.LocalPos.x + cell.LocalPos.y * mapSettings.WorldWidth].TerrainType;
                CreateHexagon(cell.WorldPos, mapData.Hexes[cell.LocalPos.x + cell.LocalPos.y * mapSettings.WorldWidth]
                                            .TerrainType.TextureIndex);
            }

            ApplyMesh();
        }

        private void ClearMesh()
        {
            mesh.Clear();
            vertices.Clear();
            triangles.Clear();
            uvs.Clear();
        }

        public override void Clear()
        {
            if (mesh != null)
            {
                ClearMesh();
                DestroyImmediate(mesh);
                DestroyImmediate(meshCollider.sharedMesh);
            }
        }

        private void ApplyMesh()
        {
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();

            meshCollider.sharedMesh = mesh;

            mesh.uv = uvs.ToArray();
        }

        /// <summary>
        /// Adds all vertices and triangles required for single hexagon shape (7 vertices, 6 triangles)
        /// </summary>
        /// <param name="center">Center of the hexagon</param>
        private void CreateHexagon(Vector3 center, int textureIndex)
        {
            int vertexIndex = vertices.Count;

            vertices.Add(center);
            SetUVs(textureIndex);

            for (int i = 0; i < HexData.HexVerticesCount; ++i)
            {
                vertices.Add(center + HexData.vertices[i]);
            }

            for (int i = 0; i < HexData.HexVerticesCount; ++i)
            {
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + i + 1);

                if (i + 2 <= HexData.HexVerticesCount)
                    triangles.Add(vertexIndex + i + 2);
                else
                    triangles.Add(vertexIndex + 1);
            }

        }

        private void SetUVs(int index)
        {
            int TexCount = shaderManager.TexturesInARow;
            int TexSize = shaderManager.SingleTextureSize;

            int indexX = index % TexCount;
            int indexY = index / TexCount;

            uvs.Add(ConvertPixelsToUVCoords((indexX * TexSize) + (TexSize / 2),
                                            (indexY * TexSize) + (TexSize / 2)));

            uvs.Add(ConvertPixelsToUVCoords((indexX * TexSize) + (TexSize / 2),
                                            (indexY * TexSize)));

            uvs.Add(ConvertPixelsToUVCoords((indexX * TexSize) + (TexSize),
                                            (indexY * TexSize) + (int)(TexSize * 0.25)));
            uvs.Add(ConvertPixelsToUVCoords((indexX * TexSize) + (TexSize),
                                            (indexY * TexSize) + (int)(TexSize * 0.75)));

            uvs.Add(ConvertPixelsToUVCoords((indexX * TexSize) + (TexSize / 2),
                                            (indexY * TexSize) + (TexSize)));

            uvs.Add(ConvertPixelsToUVCoords((indexX * TexSize),
                                            (indexY * TexSize) + (int)(TexSize * 0.75)));
            uvs.Add(ConvertPixelsToUVCoords((indexX * TexSize),
                                            (indexY * TexSize) + (int)(TexSize * 0.25)));
        }

        private Vector2 ConvertPixelsToUVCoords(int x, int y)
        {
            return new Vector2((float)x / shaderManager.AtlasSize, (float)y / shaderManager.AtlasSize);
        }
    }
}

