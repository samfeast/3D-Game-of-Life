using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GenerateStep {

    static int[] deathCondition = new int[] {};
    static int[] resurrectCondition = new int[] { 0,1 };

    public static SimulationState nextStep(bool[,,] arr) {

        bool[,,] newArr = new bool[arr.GetLength(0), arr.GetLength(1), arr.GetLength(2)];
        List<VoxelData> voxels = new List<VoxelData>();
        List<int> cumulativeFaces = new List<int>();

        // Go through all the cells and check if they're alive in the next iteration.
        for (int x = 0; x < arr.GetLength(0); x++) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int z = 0; z < arr.GetLength(2); z++) {

                    int[] neighbours = getNeighbours(arr, x, y, z);

                    if (arr[x, y, z] && deathCondition.Contains(neighbours.Length)) {
                        newArr[x, y, z] = false;
                    }
                    else if (!arr[x, y, z] && resurrectCondition.Contains(neighbours.Length)) {
                        newArr[x, y, z] = true;
                    } else {
                        newArr[x, y, z] = arr[x, y, z];
                    }

                }
            }
        }

        // Generate the voxels for the new state. This involves finding the neighbours of each alive cell.
        int externalFaces = 0;
        for (int x = 0; x < newArr.GetLength(0); x++) {
            for (int y = 0; y < newArr.GetLength(1); y++) {
                for (int z = 0; z < newArr.GetLength(2); z++) {
                    if (newArr[x, y, z]) {
                        bool bottomNeighbour = false;
                        bool topNeighbour = false;
                        bool frontNeighbour = false;
                        bool backNeighbour = false;
                        bool leftNeighbour = false;
                        bool rightNeighbour = false;

                        int[] neighbours = getNeighbours(newArr, x, y, z);
                        for (int i = 0; i < neighbours.Length; i++) {
                            // These values are unique to the way offsets is generated in getNeighbours()
                            if (neighbours[i] == 10) bottomNeighbour = true; else externalFaces++;
                            if (neighbours[i] == 15) topNeighbour = true; else externalFaces++;
                            if (neighbours[i] == 12) frontNeighbour = true; else externalFaces++;
                            if (neighbours[i] == 13) backNeighbour = true; else externalFaces++;
                            if (neighbours[i] == 4) leftNeighbour = true; else externalFaces++;
                            if (neighbours[i] == 21) rightNeighbour = true; else externalFaces++;
                        }
                        voxels.Add(new VoxelData(x, y, z, bottomNeighbour, topNeighbour, frontNeighbour, backNeighbour, leftNeighbour, rightNeighbour));
                        cumulativeFaces.Add(externalFaces);
                    }
                }
            }
        }

        return new SimulationState(newArr, voxels, cumulativeFaces, externalFaces);
    }
    private static int[] getNeighbours(bool[,,] arr, int x, int y, int z) {
        // Get all the offset vectors
        int[][] offsets = new int[26][];
        int index = 0;
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                for (int k = -1; k <= 1; k++) {
                    if (i != 0 || j != 0 || k != 0) {
                        offsets[index] = new int[] { i, j, k };
                        index++;
                    }
                }
            }
        }

        // Find out how many neighbours the 
        int countNeighbours = 0;
        for (int i = 0; i < 26; i++) {
            try {
                if (arr[x + offsets[i][0], y + offsets[i][1], z + offsets[i][2]]) countNeighbours++;
            } catch (IndexOutOfRangeException) {
                // Index is outside the range of the array
            }
        }

        int[] neighbours = new int[countNeighbours];
        int neighboursFound = 0;
        for (int i = 0; i < 26; i++) {
            try {
                if (arr[x + offsets[i][0], y + offsets[i][1], z + offsets[i][2]]) {
                    neighbours[neighboursFound] = i;
                    neighboursFound++;
                }
            }
            catch (IndexOutOfRangeException) {
                // Index is outside the range of the array
            }
        }

        return neighbours;
    }

}
