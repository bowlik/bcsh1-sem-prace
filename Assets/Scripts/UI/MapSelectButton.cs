using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapSelectButton : MonoBehaviour
{
    [Header("Mapa")]
    public string sceneName = "GameScene_Goldmine";
    public string mapName = "Goldmine";

    [Header("UI")]
    public Image previewImage;
    public Texture2D previewTexture;
    public TextMeshProUGUI nameText;

    private void Start()
    {
        if (nameText != null)
            nameText.text = mapName;

        if (previewImage != null && previewTexture != null)
            previewImage.sprite = Sprite.Create(
                previewTexture,
                new Rect(0, 0, previewTexture.width, previewTexture.height),
                new Vector2(0.5f, 0.5f)
            );

        GetComponent<Button>().onClick.AddListener(OnSelect);
    }

    private void OnSelect()
    {
        MapSelector.SelectedScene = sceneName;
        Debug.Log($"Vybrána mapa: {sceneName}");

        // zvýrazni vybrané tlaèítko
        foreach (var btn in FindObjectsByType<MapSelectButton>(FindObjectsSortMode.None))
            btn.GetComponent<Image>().color = Color.white;

        GetComponent<Image>().color = new Color(0.5f, 1f, 0.5f);
    }
}