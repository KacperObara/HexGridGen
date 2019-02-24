using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MeshGen : MonoBehaviour
    {
        Mesh mesh;
        List<Vector3> vertices;
        List<int> triangles;

        private void Awake()
        {
            vertices = new List<Vector3>();
            triangles = new List<int>();

            mesh = GetComponent<MeshFilter>().mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // allows for meshes bigger than ~65000 vertices
        }

        public void Triangulate(Hex[] cells)
        {
            mesh.Clear();
            vertices.Clear();
            triangles.Clear();
            for (int i = 0; i < cells.Length; i++)
            {
                Triangulate(cells[i]);
            }
            mesh.vertices = vertices.ToArray();
            Debug.Log(mesh.vertexCount);
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
            
        }
        Vector3 center;
        private void Triangulate(Hex cell)
        {
            center = cell.transform.localPosition;
            AddAllTriangles();
            //for (int i = 0; i < 6; ++i)
            //{
            //    AddTriangle(
            //        center,
            //        center + Hex.vertices[i],
            //        center + Hex.vertices[i + 1]
            //    );
            //}
        }

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
        /// 
        /// </summary>
        private void AddAllTriangles()
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

