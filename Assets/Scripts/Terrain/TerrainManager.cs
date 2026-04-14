using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public static TerrainManager Instance { get; private set; }

    [Header("Terén")]
    public Texture2D terrainTexture;
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D terrainCollider;

    private Texture2D _runtimeTexture;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        // vytvoøíme kopii textury, abychom mohli mìnit pixely
        _runtimeTexture = Instantiate(terrainTexture);
        spriteRenderer.sprite = Sprite.Create(
            _runtimeTexture,
            new Rect(0, 0, _runtimeTexture.width, _runtimeTexture.height),
            new Vector2(0.5f, 0.5f),
            100f
        );
    }

    /// <summary>
    /// Znièí terén v kruhu kolem worldPosition s daným polomìrem.
    /// </summary>
    public void DestroyTerrain(Vector2 worldPosition, float radius)
    {
        // pøevod world souøadnic na pixel souøadnice
        Vector2 textureSize = new Vector2(_runtimeTexture.width, _runtimeTexture.height);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        int centerX = Mathf.RoundToInt(
            (worldPosition.x / spriteSize.x + 0.5f) * textureSize.x);
        int centerY = Mathf.RoundToInt(
            (worldPosition.y / spriteSize.y + 0.5f) * textureSize.y);

        int pixelRadius = Mathf.RoundToInt(radius * (textureSize.x / spriteSize.x));

        // vymaž pixely v kruhu
        for (int x = centerX - pixelRadius; x <= centerX + pixelRadius; x++)
        {
            for (int y = centerY - pixelRadius; y <= centerY + pixelRadius; y++)
            {
                if (x < 0 || x >= _runtimeTexture.width ||
                    y < 0 || y >= _runtimeTexture.height) continue;

                float dist = Vector2.Distance(new Vector2(x, y), new Vector2(centerX, centerY));
                if (dist <= pixelRadius)
                    _runtimeTexture.SetPixel(x, y, Color.clear);
            }
        }

        _runtimeTexture.Apply();
        // TODO: aktualizovat PolygonCollider2D po výbuchu
    }
}