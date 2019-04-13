using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMeshGen : MonoBehaviour
    {
        Mesh mesh;
        MeshCollider meshCollider;
        List<Vector3> vertices;
        List<int> triangles;
        List<Color32> appliedColors; //Color32 is more performant

        public List<Color32> Colors; 

        private void Awake()
        {
            vertices = new List<Vector3>();
            triangles = new List<int>();
            appliedColors = new List<Color32>();

            mesh = GetComponent<MeshFilter>().mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // allows for meshes bigger than ~65000 vertices

            meshCollider = gameObject.AddComponent<MeshCollider>();
        }

        public void ClearMesh()
        {
            mesh.Clear();
            vertices.Clear();
            triangles.Clear();
            appliedColors.Clear();
        }

        public void ApplyMesh()
        {
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();

            meshCollider.sharedMesh = mesh;
            meshCollider.sharedMesh.colors32 = appliedColors.ToArray();
        }

        /// <summary>
        /// Clear mesh, generate hexagons and assign them to mesh
        /// </summary>
        /// <param name="cells"></param>
        public void Triangulate(Hex[] cells)
        {
            ClearMesh();

            foreach (Hex cell in cells)
            {
                int tmp = Random.Range(0, 2);
                CreateHexagon(cell.WorldPosition, Colors[tmp]);
            }

            ApplyMesh();
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
            for (int i = 0; i < 6; ++i)
            {
                vertices.Add(center + HexInfo.vertices[i]);
                appliedColors.Add(color);
            }

            for (int i = 0; i < 6; ++i)
            {
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + i + 1);

                if (i + 2 <= 6)
                    triangles.Add(vertexIndex + i + 2);
                else
                    triangles.Add(vertexIndex + 1);
            }
        }
    }
}

