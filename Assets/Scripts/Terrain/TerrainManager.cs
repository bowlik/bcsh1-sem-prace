using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainManager : MonoBehaviour
{
    public static TerrainManager Instance { get; private set; }

    [Header("Tilemap")]
    public Tilemap terrainTilemap;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void DestroyTerrain(Vector2 worldPosition, float radius)
    {
        int pixelRadius = Mathf.RoundToInt(radius);

        for (int x = -pixelRadius; x <= pixelRadius; x++)
        {
            for (int y = -pixelRadius; y <= pixelRadius; y++)
            {
                float dist = Vector2.Distance(Vector2.zero, new Vector2(x, y));
                if (dist > pixelRadius) continue;

                Vector3Int cellPos = terrainTilemap.WorldToCell(
                    new Vector3(
                        worldPosition.x + x * 0.32f,
                        worldPosition.y + y * 0.32f,
                        0));

                terrainTilemap.SetTile(cellPos, null);
            }
        }
    }
}