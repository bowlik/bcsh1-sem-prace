using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapSelectButton : MonoBehaviour
{
    [Header("Mapa")]
    public Texture2D mapTexture;
    public string mapName = "Mapa";

    [Header("UI")]
    public Image previewImage;
    public TextMeshProUGUI nameText;

    private void Start()
    {
        if (nameText != null)
            nameText.text = mapName;

        if (previewImage != null && mapTexture != null)
            previewImage.sprite = Sprite.Create(
                mapTexture,
                new Rect(0, 0, mapTexture.width, mapTexture.height),
                new Vector2(0.5f, 0.5f)
            );

        GetComponent<Button>().onClick.AddListener(OnSelect);
    }

    private void OnSelect()
    {
        MapSelector.SelectedMap = mapTexture;
        Debug.Log($"Vybrána mapa: {mapName}");

        // zvýrazni vybrané tlaèítko
        foreach (var btn in FindObjectsByType<MapSelectButton>(FindObjectsSortMode.None))
            btn.GetComponent<Image>().color = Color.white;

        GetComponent<Image>().color = new Color(0.5f, 1f, 0.5f); // zelená = vybraná
    }
}