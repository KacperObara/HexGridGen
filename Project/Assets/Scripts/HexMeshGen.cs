using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu]
    public class HexMeshGen : MeshGen
    {
        private HexGrid grid;
        private Mesh mesh;
        private MeshCollider meshCollider;

        private List<Vector3> vertices;
        private List<int> triangles;
        private List<Color32> appliedColors; //Color32 is more performant than Color

        public override void Initialize(Generator generator)
        {
            this.grid = generator.HexGrid;
            this.meshCollider = generator.GetComponent<MeshCollider>();

            vertices = new List<Vector3>();
            triangles = new List<int>();
            appliedColors = new List<Color32>();

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

            foreach (Hex cell in grid.Hexes)
            {
                CreateHexagon(cell.WorldPos, grid.ColorMap[cell.LocalPos.x, cell.LocalPos.y]);
            }

            ApplyMesh();
        }

        private void ClearMesh()
        {
            mesh.Clear();
            vertices.Clear();
            triangles.Clear();
            appliedColors.Clear();
        }

        private void ApplyMesh()
        {
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();

            meshCollider.sharedMesh = mesh;
            meshCollider.sharedMesh.colors32 = appliedColors.ToArray();
        }

        /// <summary>
        /// Adds single triangle using specified vertices positions
        /// Not used currently, because of CreateHexagon function, but useful, if you
        /// want every triangle to have unique vertices
        /// </summary>
        /// <param name="v1">vertex nr 1</param>
        /// <param name="v2">vertex nr 2</param>
        /// <param name="v3">vertex nr 3</param>
        private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2); // no colors at the moment
        }

        /// <summary>
        /// Adds all vertices and triangles required for single hexagon shape (7 vertices, 6 triangles)
        /// </summary>
        /// <param name="center">Center of the hexagon</param>
        private void CreateHexagon(Vector3 center, Color32 color)
        {
            int vertexIndex = vertices.Count;

            vertices.Add(center);
            appliedColors.Add(color);
            for (int i = 0; i < HexInfo.HexVerticesCount; ++i)
            {
                vertices.Add(center + HexInfo.vertices[i]);
                appliedColors.Add(color);
            }

            for (int i = 0; i < HexInfo.HexVerticesCount; ++i)
            {
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + i + 1);

                if (i + 2 <= HexInfo.HexVerticesCount)
                    triangles.Add(vertexIndex + i + 2);
                else
                    triangles.Add(vertexIndex + 1);
            }
        }
    }
}

