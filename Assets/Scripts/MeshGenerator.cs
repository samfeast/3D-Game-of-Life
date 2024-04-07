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

    int verticesFound = 0;
    int trianglesFound = 0;

    List<VoxelData> voxels = new List<VoxelData>();

    private void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        bool[,,] arr = new bool[20, 20, 20];
        arr[0, 0, 0] = true;

        // Find how many cells are alive
        int numVoxels = 0;
        for (int x = 0; x < arr.GetLength(0); x++) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int z = 0; z < arr.GetLength(2); z++) {
                    if (arr[x, y, z]) numVoxels++;
                }
            }
        }

        SimulationState newState = GenerateStep.nextStep(arr);
        voxels = newState.voxels;
        int externalFaces = newState.externalFaces;

        print("Generated " + voxels.Count + " voxels.");
        MakeMeshData(voxels, externalFaces);
        CreateMesh();
    }

    void MakeMeshData(List<VoxelData> voxels, int externalFaces) {
        vertices = new Vector3[4 * externalFaces];
        triangles = new int[6 * externalFaces];

        for (int i = 0; i < voxels.Count; i++) {
            int x = voxels[i].x;
            int y = voxels[i].y;
            int z = voxels[i].z;

            if (!voxels[i].bottomNeighbour) {
                vertices[verticesFound] = new Vector3(x, y, z);
                vertices[verticesFound + 1] = new Vector3(x + 1, y, z);
                vertices[verticesFound + 2] = new Vector3(x, y, z + 1);
                vertices[verticesFound + 3] = new Vector3(x + 1, y, z + 1);

                SetTriangles();
            }

            if (!voxels[i].topNeighbour) {
                vertices[verticesFound] = new Vector3(x, y + 1, z);
                vertices[verticesFound + 1] = new Vector3(x, y + 1, z + 1);
                vertices[verticesFound + 2] = new Vector3(x + 1, y + 1, z);
                vertices[verticesFound + 3] = new Vector3(x + 1, y + 1, z + 1);
                SetTriangles();
            }

            if (!voxels[i].frontNeighbour) {
                vertices[verticesFound] = new Vector3(x, y, z);
                vertices[verticesFound + 1] = new Vector3(x, y + 1, z);
                vertices[verticesFound + 2] = new Vector3(x + 1, y, z);
                vertices[verticesFound + 3] = new Vector3(x + 1, y + 1, z);

                SetTriangles();
            }

            if (!voxels[i].backNeighbour) {
                vertices[verticesFound] = new Vector3(x, y, z + 1);
                vertices[verticesFound + 1] = new Vector3(x + 1, y, z + 1);
                vertices[verticesFound + 2] = new Vector3(x, y + 1, z + 1);
                vertices[verticesFound + 3] = new Vector3(x + 1, y + 1, z + 1);

                SetTriangles();
            }

            if (!voxels[i].leftNeighbour) {
                vertices[verticesFound] = new Vector3(x, y, z);
                vertices[verticesFound + 1] = new Vector3(x, y, z + 1);
                vertices[verticesFound + 2] = new Vector3(x, y + 1, z);
                vertices[verticesFound + 3] = new Vector3(x, y + 1, z + 1);

                SetTriangles();
            }

            if (!voxels[i].rightNeighbour) {
                vertices[verticesFound] = new Vector3(x + 1, y, z);
                vertices[verticesFound + 1] = new Vector3(x + 1, y + 1, z);
                vertices[verticesFound + 2] = new Vector3(x + 1, y, z + 1);
                vertices[verticesFound + 3] = new Vector3(x + 1, y + 1, z + 1);

                SetTriangles();
            }
        }
    }

    void SetTriangles() {
        triangles[trianglesFound] = verticesFound;
        triangles[trianglesFound + 1] = verticesFound + 1;
        triangles[trianglesFound + 2] = verticesFound + 2;
        triangles[trianglesFound + 3] = verticesFound + 2;
        triangles[trianglesFound + 4] = verticesFound + 1;
        triangles[trianglesFound + 5] = verticesFound + 3;
        verticesFound += 4;
        trianglesFound += 6;
    }

    void CreateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
