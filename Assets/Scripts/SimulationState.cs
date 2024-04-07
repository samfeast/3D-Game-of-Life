using System;
using System.Collections.Generic;

public class SimulationState {

    public bool[,,] arr;
    public List<VoxelData> voxels;
    public int externalFaces;

    public SimulationState(bool[,,] arr, List<VoxelData> voxels, int externalFaces) {
        this.arr = arr;
        this.voxels = voxels;
        this.externalFaces = externalFaces;
    }
}
