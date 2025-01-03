
using UnityEngine;

public class PerlinTerrain : MonoBehaviour
{
    public int width = 512; // Terrain width
    public int height = 512; // Terrain height
    public int depth = 50; // Terrain depth
    public float scale = 30.0f; // Perlin noise scale

    public Vector2 flatRegionStart = new Vector2(50, 50);
    public Vector2 flatRegionEnd = new Vector2(200, 200);
    public float flatHeight = 0.2f; // Height of the flat region

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        // Refresh the terrain dat  a
        terrain.Flush();
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // Check if within the flat region
                if (x >= flatRegionStart.x && x <= flatRegionEnd.x &&
                    z >= flatRegionStart.y && z <= flatRegionEnd.y)
                {
                    heights[x, z] = flatHeight; // Set flat height
                }
                else
                {
                    // Generate Perlin noise for mountains
                    float xCoord = (float)x / width * scale;
                    float zCoord = (float)z / height * scale;
                    heights[x, z] = Mathf.PerlinNoise(xCoord, zCoord);
                }
            }
        }

        return heights;
    }
}

