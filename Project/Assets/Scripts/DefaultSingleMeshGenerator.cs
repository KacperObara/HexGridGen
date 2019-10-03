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

        private List<Vector3> vertices;
        private List<int> triangles;

        private List<Vector2> uvs;

        private int textureSize = 590; // Make it a variable for user later 

        public override void Initialize(Generator generator)
        {
            this.mapData = generator.MapData;
            this.mapSettings = generator.MapSettings;
            this.meshCollider = generator.GetComponent<MeshCollider>();

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
                CreateHexagon(cell.WorldPos, mapData.TextureIndex[cell.LocalPos.x + cell.LocalPos.y * mapSettings.WorldWidth]);
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
            int indexX = index % 3;
            int indexY = index / 3;

            uvs.Add(ConvertPixelsToUVCoords(256 + (indexX * textureSize), 295 + (indexY * textureSize)));
            uvs.Add(ConvertPixelsToUVCoords(256 + (indexX * textureSize), 0 + (indexY * textureSize)));

            uvs.Add(ConvertPixelsToUVCoords(1 + (indexX * textureSize), 148 + (indexY * textureSize)));
            uvs.Add(ConvertPixelsToUVCoords(1 + (indexX * textureSize), 442 + (indexY * textureSize)));

            uvs.Add(ConvertPixelsToUVCoords(256 + (indexX * textureSize), 590 + (indexY * textureSize)));

            uvs.Add(ConvertPixelsToUVCoords(511 + (indexX * textureSize), 442 + (indexY * textureSize)));
            uvs.Add(ConvertPixelsToUVCoords(511 + (indexX * textureSize), 148 + (indexY * textureSize)));
        }

        private Vector2 ConvertPixelsToUVCoords(int x, int y)
        {
            int atlasSize = 3 * textureSize;
            return new Vector2((float)x / atlasSize, (float)y / atlasSize);
        }
    }
}

