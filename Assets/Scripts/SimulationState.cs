using System;
using System.Collections.Generic;

public class SimulationState {

    public bool[,,] arr;
    public List<VoxelData> voxels;
    public List<int> cumulativeFaces;
    public int externalFaces;

    public SimulationState(bool[,,] arr, List<VoxelData> voxels, List<int> cumulativeFaces, int externalFaces) {
        this.arr = arr;
        this.voxels = voxels;
        this.cumulativeFaces = cumulativeFaces;
        this.externalFaces = externalFaces;
    }
}