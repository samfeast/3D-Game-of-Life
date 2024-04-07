using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    VoxelData[] voxels;

    private void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        bool[,,] arr = new bool[3, 3, 3];
        arr[1, 1, 1] = true;

        // Find how many cells are alive
        int numVoxels = 0;
        for (int x = 0; x < arr.GetLength(0); x++) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int z = 0; z < arr.GetLength(2); z++) {
                    if (arr[x, y, z]) numVoxels++;
                }
            }
        }

        voxels = new VoxelData[numVoxels];
        int found = 0;
        for (int x = 0; x < arr.GetLength(0); x++) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int z = 0; z < arr.GetLength(2); z++) {
                    if (arr[x, y, z]) {
                        voxels[found] = new VoxelData(x, y, z);
                        found++;
                    }
                }
            }
        }
        print("Generated " + found + " voxels.");
        MakeMeshData();
        CreateMesh();
    }

    void MakeMeshData() {
        vertices = new Vector3[24 * voxels.Length];
        triangles = new int[36 * voxels.Length];

        int[] tris = new int[] { 0, 1, 2, 2, 1, 3, 4, 5, 6, 6, 5, 7, 8, 9, 10, 10, 9, 11, 12, 13, 14, 14, 13, 15, 16, 17, 18, 18, 17, 19, 20, 21, 22, 22, 21, 23 };
        for (int i = 0; i < voxels.Length; i++) {
            int x = voxels[i].x;
            int y = voxels[i].y;
            int z = voxels[i].z;

            // Bottom face
            vertices[i * 24 + 0] = new Vector3(x, y, z);
            vertices[i * 24 + 1] = new Vector3(x + 1, y, z);
            vertices[i * 24 + 2] = new Vector3(x, y, z + 1);
            vertices[i * 24 + 3] = new Vector3(x + 1, y, z + 1);
            // Top face
            vertices[i * 24 + 4] = new Vector3(x, y + 1, z);
            vertices[i * 24 + 5] = new Vector3(x, y + 1, z + 1);
            vertices[i * 24 + 6] = new Vector3(x + 1, y + 1, z);
            vertices[i * 24 + 7] = new Vector3(x + 1, y + 1, z + 1);
            // Front face
            vertices[i * 24 + 8] = new Vector3(x, y, z);
            vertices[i * 24 + 9] = new Vector3(x, y + 1, z);
            vertices[i * 24 + 10] = new Vector3(x + 1, y, z);
            vertices[i * 24 + 11] = new Vector3(x + 1, y + 1, z);
            // Back face
            vertices[i * 24 + 12] = new Vector3(x, y, z + 1);
            vertices[i * 24 + 13] = new Vector3(x + 1, y, z + 1);
            vertices[i * 24 + 14] = new Vector3(x, y + 1, z + 1);
            vertices[i * 24 + 15] = new Vector3(x + 1, y + 1, z + 1);
            // Left face
            vertices[i * 24 + 16] = new Vector3(x, y, z);
            vertices[i * 24 + 17] = new Vector3(x, y, z + 1);
            vertices[i * 24 + 18] = new Vector3(x, y + 1, z);
            vertices[i * 24 + 19] = new Vector3(x, y + 1, z + 1);
            // Right face
            vertices[i * 24 + 20] = new Vector3(x + 1, y, z);
            vertices[i * 24 + 21] = new Vector3(x + 1, y + 1, z);
            vertices[i * 24 + 22] = new Vector3(x + 1, y, z + 1);
            vertices[i * 24 + 23] = new Vector3(x + 1, y + 1, z + 1);

            for (int j = 0; j < tris.Length; j++) {
                triangles[i * tris.Length + j] = i * 24 + tris[j];
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
