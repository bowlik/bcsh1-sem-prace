using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainManager : MonoBehaviour
{
    public static TerrainManager Instance { get; private set; }

    [Header("Tilemap")]
    public Tilemap terrainTilemap;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (terrainTilemap == null)
            Debug.LogError("TerrainManager: Terrain Tilemap není přiřazený!");
        else
            Debug.Log("TerrainManager: inicializován správně ✅");
    }

    public void DestroyTerrain(Vector2 worldPosition, float radius)
    {
        if (terrainTilemap == null)
        {
            Debug.LogError("TerrainManager: terrainTilemap je null!");
            return;
        }

        Debug.Log($"DestroyTerrain: pozice {worldPosition}, radius {radius}");

        int pixelRadius = Mathf.RoundToInt(radius * 2f);

        int tilesRemoved = 0;

        for (int x = -pixelRadius; x <= pixelRadius; x++)
        {
            for (int y = -pixelRadius; y <= pixelRadius; y++)
            {
                float dist = Vector2.Distance(
                    Vector2.zero, new Vector2(x, y));

                if (dist > pixelRadius) continue;

                Vector3Int cellPos = terrainTilemap.WorldToCell(
                    new Vector3(
                        worldPosition.x + x * 0.5f,
                        worldPosition.y + y * 0.5f,
                        0));

                if (terrainTilemap.GetTile(cellPos) != null)
                {
                    terrainTilemap.SetTile(cellPos, null);
                    tilesRemoved++;
                }
            }
        }

        Debug.Log($"DestroyTerrain: odstraněno {tilesRemoved} tilů");
    }
}