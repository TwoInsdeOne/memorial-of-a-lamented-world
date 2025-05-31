using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WaterSurfaceMesh : MonoBehaviour
{
    public List<Vector3> wavePoints = new List<Vector3>(); // Pega os pontos da onda
    public float depth = 2f; // profundidade da transparência

    private Mesh mesh;
    private WaterSurface surface;
    public bool populated;

    void Start()
    {
        populated = false;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().sortingLayerName = "water";
        surface = GetComponent<WaterSurface>();
    }

    void Update()
    {
        
        UpdateMesh();
    }

    void UpdateMesh()
    {
        if (wavePoints == null || wavePoints.Count < 2) return;

        Vector3[] vertices = new Vector3[wavePoints.Count * 2];
        int[] triangles = new int[(wavePoints.Count - 1) * 6];

        for (int i = 0; i < wavePoints.Count; i++)
        {
            Vector3 point = wavePoints[i];
            vertices[i * 2] = point; // topo
            vertices[i * 2 + 1] = point + Vector3.down * depth; // fundo
        }

        int ti = 0;
        for (int i = 0; i < wavePoints.Count - 1; i++)
        {
            int idx = i * 2;

            // Primeiro triângulo
            triangles[ti++] = idx;
            triangles[ti++] = idx + 1;
            triangles[ti++] = idx + 2;

            // Segundo triângulo
            triangles[ti++] = idx + 2;
            triangles[ti++] = idx + 1;
            triangles[ti++] = idx + 3;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    public void PopulateWave()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            wavePoints.Add(transform.GetChild(i).position);
            transform.GetChild(i).GetComponent<FollowWave>().id = i;
        }
        populated = true;
    }
}