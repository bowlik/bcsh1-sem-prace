using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("Panely")]
    public GameObject scorePanel;
    public GameObject mapPanel;

    [Header("Texty")]
    public TextMeshProUGUI scoreText;

    [Header("Výbìr mapy")]
    public MapSelectButton[] mapButtons;

    private void Start()
    {
        scorePanel.SetActive(false);
        mapPanel.SetActive(false);

        // nastav výchozí mapu
        if (MapSelector.SelectedMap == null && mapButtons.Length > 0)
            MapSelector.SelectedMap = mapButtons[0].mapTexture;
    }

    // --- Tlaèítka ---

    public void OnPlayClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnMapClicked()
    {
        mapPanel.SetActive(!mapPanel.activeSelf);
        scorePanel.SetActive(false);
    }

    public void OnScoreClicked()
    {
        scorePanel.SetActive(!scorePanel.activeSelf);
        mapPanel.SetActive(false);
        RefreshScore();
    }

    public void OnQuitClicked()
    {
        Application.Quit();
        Debug.Log("Hra ukonèena");
    }

    // --- Skóre ---

    private void RefreshScore()
    {
        if (ScoreManager.Instance == null || scoreText == null) return;

        var data = ScoreManager.Instance.GetData();
        scoreText.text =
            $"Hráè 1: {data.player1Wins} výher\n" +
            $"Hráè 2: {data.player2Wins} výher\n\n" +
            "--- Poslední zápasy ---\n";

        int start = Mathf.Max(0, data.history.Count - 5);
        for (int i = start; i < data.history.Count; i++)
        {
            var match = data.history[i];
            scoreText.text += $"{match.date}  –  Vyhrál Hráè {match.winner}\n";
        }
    }
}