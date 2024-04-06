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
        voxels = new VoxelData[] { new VoxelData(0, 0, 0) };
        MakeMeshData();
        CreateMesh();
    }

    void MakeMeshData() {
        vertices = new Vector3[36 * voxels.Length];
        triangles = new int[36 * voxels.Length];
        for (int i = 0; i < voxels.Length; i++) {
            int x = voxels[i].x;
            int y = voxels[i].y;
            int z = voxels[i].z;
            // Bottom face
            vertices[i * voxels.Length + 0] = new Vector3(x, y, z);
            vertices[i * voxels.Length + 1] = new Vector3(x + 1, y, z);
            vertices[i * voxels.Length + 2] = new Vector3(x, y, z + 1);
            vertices[i * voxels.Length + 3] = new Vector3(x, y, z + 1);
            vertices[i * voxels.Length + 4] = new Vector3(x + 1, y, z);
            vertices[i * voxels.Length + 5] = new Vector3(x + 1, y, z + 1);
            // Top face
            vertices[i * voxels.Length + 6] = new Vector3(x, y + 1, z);
            vertices[i * voxels.Length + 7] = new Vector3(x, y + 1, z + 1);
            vertices[i * voxels.Length + 8] = new Vector3(x + 1, y + 1, z);
            vertices[i * voxels.Length + 9] = new Vector3(x + 1, y + 1, z);
            vertices[i * voxels.Length + 10] = new Vector3(x, y + 1, z + 1);
            vertices[i * voxels.Length + 11] = new Vector3(x + 1, y + 1, z + 1);
            // Front face
            vertices[i * voxels.Length + 12] = new Vector3(x, y, z);
            vertices[i * voxels.Length + 13] = new Vector3(x, y, z + 1);
            vertices[i * voxels.Length + 14] = new Vector3(x, y + 1, z + 1);
            vertices[i * voxels.Length + 15] = new Vector3(x, y + 1, z + 1);
            vertices[i * voxels.Length + 16] = new Vector3(x, y + 1, z);
            vertices[i * voxels.Length + 17] = new Vector3(x, y, z);
            // Back face
            vertices[i * voxels.Length + 18] = new Vector3(x + 1, y, z);
            vertices[i * voxels.Length + 19] = new Vector3(x + 1, y + 1, z + 1);
            vertices[i * voxels.Length + 20] = new Vector3(x + 1, y, z + 1);
            vertices[i * voxels.Length + 21] = new Vector3(x + 1, y + 1, z);
            vertices[i * voxels.Length + 22] = new Vector3(x + 1, y + 1, z + 1);
            vertices[i * voxels.Length + 23] = new Vector3(x + 1, y, z);
            // Left face
            vertices[i * voxels.Length + 24] = new Vector3(x, y, z + 1);
            vertices[i * voxels.Length + 25] = new Vector3(x + 1, y, z + 1);
            vertices[i * voxels.Length + 26] = new Vector3(x + 1, y + 1, z + 1);
            vertices[i * voxels.Length + 27] = new Vector3(x + 1, y + 1, z + 1);
            vertices[i * voxels.Length + 28] = new Vector3(x, y + 1, z + 1);
            vertices[i * voxels.Length + 29] = new Vector3(x, y, z + 1);
            // Right face
            vertices[i * voxels.Length + 30] = new Vector3(x, y, z);
            vertices[i * voxels.Length + 31] = new Vector3(x + 1, y + 1, z);
            vertices[i * voxels.Length + 32] = new Vector3(x + 1, y, z);
            vertices[i * voxels.Length + 33] = new Vector3(x, y + 1, z);
            vertices[i * voxels.Length + 34] = new Vector3(x + 1, y + 1, z);
            vertices[i * voxels.Length + 35] = new Vector3(x, y, z);


            for (int j = 0; j < 36; j++) {
                triangles[i * voxels.Length + j] = i * voxels.Length + j;
            }
            

        }

    }

    void CreateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
