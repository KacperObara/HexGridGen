using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MeshGen : MonoBehaviour
    {
        Mesh mesh;
        MeshCollider meshCollider;
        List<Vector3> vertices;
        List<int> triangles;

        private void Awake()
        {
            vertices = new List<Vector3>();
            triangles = new List<int>();

            mesh = GetComponent<MeshFilter>().mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // allows for meshes bigger than ~65000 vertices

            meshCollider = gameObject.AddComponent<MeshCollider>();
        }

        /// <summary>
        /// Clear mesh, generate hexagons and assign them to mesh
        /// </summary>
        /// <param name="cells"></param>
        public void Triangulate(Hex[] cells)
        {
            mesh.Clear();
            vertices.Clear();
            triangles.Clear();

            for (int i = 0; i < cells.Length; i++)
            {
                CreateHexagon(cells[i].WorldPosition);
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();

            meshCollider.sharedMesh = mesh;
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
            triangles.Add(vertexIndex + 2);
        }

        /// <summary>
        /// Adds all vertices and triangles required for single hexagon shape (7 vertices, 6 triangles)
        /// </summary>
        /// <param name="center">Center of the hexagon</param>
        private void CreateHexagon(Vector3 center)
        {
            int vertexIndex = vertices.Count;

            vertices.Add(center);
            for (int i = 0; i < 6; ++i)
            {
                vertices.Add(center + Hex.vertices[i]);
            }

            for (int i = 0; i < 6; ++i)
            {
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + i + 1);

                if (i+2 <= 6)
                    triangles.Add(vertexIndex + i + 2);
                else
                    triangles.Add(vertexIndex + 1);
            }
        }
    }
}

