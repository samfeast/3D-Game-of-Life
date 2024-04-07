using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    List<VoxelData> voxels = new List<VoxelData>();

    private void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        bool[,,] arr = new bool[1, 3, 1];
        arr[0, 1, 0] = true;

        // Find how many cells are alive
        int numVoxels = 0;
        for (int x = 0; x < arr.GetLength(0); x++) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int z = 0; z < arr.GetLength(2); z++) {
                    if (arr[x, y, z]) numVoxels++;
                }
            }
        }

        (bool[,,] newArr, List<VoxelData> voxels) = GenerateStep.nextStep(arr);

        print("Generated " + voxels.Count + " voxels.");
        MakeMeshData(voxels);
        CreateMesh();
    }

    void MakeMeshData(List<VoxelData> voxels) {
        print(voxels.Count);
        vertices = new Vector3[8 * voxels.Count];
        triangles = new int[36 * voxels.Count];

        int[] tris = new int[] { 1, 0, 5, 5, 0, 4, 7, 6, 3, 3, 6, 2, 0, 2, 4, 4, 2, 6, 5, 7, 1, 1, 7, 3, 1, 3, 0, 0, 3, 2, 4, 6, 5, 5, 6, 7 };
        for (int i = 0; i < voxels.Count; i++) {
            int x = voxels[i].x;
            int y = voxels[i].y;
            int z = voxels[i].z;

            vertices[i * 8 + 0] = new Vector3(x, y, z);
            vertices[i * 8 + 1] = new Vector3(x, y, z + 1);
            vertices[i * 8 + 2] = new Vector3(x, y + 1, z);
            vertices[i * 8 + 3] = new Vector3(x, y + 1, z + 1);
            vertices[i * 8 + 4] = new Vector3(x + 1, y, z);
            vertices[i * 8 + 5] = new Vector3(x + 1, y, z + 1);
            vertices[i * 8 + 6] = new Vector3(x + 1, y + 1, z);
            vertices[i * 8 + 7] = new Vector3(x + 1, y + 1, z + 1);

            for (int j = 0; j < tris.Length; j++) {
                triangles[i * tris.Length + j] = i * 8 + tris[j];
            }
            
        }
    }

    void CreateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
