using System.Security.Cryptography.X509Certificates;

public class VoxelData {
    public int x;
    public int y;
    public int z;

    public bool bottomNeighbour;
    public bool topNeighbour;
    public bool frontNeighbour;
    public bool backNeighbour;
    public bool leftNeighbour;
    public bool rightNeighbour;

    public VoxelData(int x, int y, int z, bool bottomNeighbour, bool topNeighbour, bool frontNeighbour, bool backNeighbour, bool leftNeighbour, bool rightNeighbour) {
        this.x = x;
        this.y = y;
        this.z = z;

        this.bottomNeighbour = bottomNeighbour;
        this.topNeighbour = topNeighbour;
        this.frontNeighbour = frontNeighbour;
        this.backNeighbour = backNeighbour;
        this.leftNeighbour = leftNeighbour;
        this.rightNeighbour = rightNeighbour;
    }
}