using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainManager : MonoBehaviour
{
    public static TerrainManager Instance { get; private set; }

    [Header("Tilemaps")]
    public Tilemap terrainTilemap;
    public Tilemap backgroundTilemap;

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
        {
            Debug.LogError("TerrainManager: Terrain Tilemap není přiřazený!");
            return;
        }

        if (backgroundTilemap != null)
            CopyTerrainToBackground();
    }

    private void CopyTerrainToBackground()
    {
        BoundsInt bounds = terrainTilemap.cellBounds;
        int copied = 0;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = terrainTilemap.GetTile(pos);
            if (tile != null)
            {
                backgroundTilemap.SetTile(pos, tile);
                backgroundTilemap.SetColor(pos, new Color(0.4f, 0.2f, 0.1f, 1f));
                copied++;
            }
        }

        Debug.Log($"Pozadí zkopírováno – {copied} tilů ✅");
    }

    public void DestroyTerrain(Vector2 worldPosition, float radius)
    {
        if (terrainTilemap == null) return;

        Vector3 scale = terrainTilemap.transform.lossyScale;
        Vector3 cellSize = terrainTilemap.cellSize;
        float realCellWidth = cellSize.x * scale.x;

        Vector3Int centerCell = terrainTilemap.WorldToCell(worldPosition);
        int searchRadius = Mathf.CeilToInt(radius / realCellWidth) + 2;
        int tilesRemoved = 0;

        for (int x = -searchRadius; x <= searchRadius; x++)
        {
            for (int y = -searchRadius; y <= searchRadius; y++)
            {
                Vector3Int cellPos = new Vector3Int(
                    centerCell.x + x,
                    centerCell.y + y,
                    centerCell.z);

                Vector3 worldCellCenter = terrainTilemap.GetCellCenterWorld(cellPos);
                float dist = Vector2.Distance(worldPosition, worldCellCenter);

                if (dist > radius) continue;

                if (terrainTilemap.GetTile(cellPos) != null)
                {
                    terrainTilemap.SetTile(cellPos, null);
                    tilesRemoved++;
                }
            }
        }

        Debug.Log($"Odstraněno {tilesRemoved} tilů");
    }
}