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

    private void Start()
    {
        if (scorePanel != null) scorePanel.SetActive(false);
        if (mapPanel != null) mapPanel.SetActive(false);
    }

    public void OnPlayClicked()
    {
        string scene = MapSelector.SelectedScene;
        if (string.IsNullOrEmpty(scene))
            scene = "GameScene_Goldmine";
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void OnMapClicked()
    {
        if (mapPanel != null)
            mapPanel.SetActive(!mapPanel.activeSelf);
        if (scorePanel != null)
            scorePanel.SetActive(false);
    }

    public void OnScoreClicked()
    {
        if (scorePanel != null)
            scorePanel.SetActive(!scorePanel.activeSelf);
        if (mapPanel != null)
            mapPanel.SetActive(false);
        RefreshScore();
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    private void RefreshScore()
    {
        if (ScoreManager.Instance == null || scoreText == null) return;

        var data = ScoreManager.Instance.GetData();

        if (data.player1Wins == 0 && data.player2Wins == 0 && data.history.Count == 0)
        {
            scoreText.text = "Zatím nebyly odehrány žádné zápasy!\n\nZahraj si první hru\na výsledky se zobrazí zde.";
            return;
        }

        scoreText.text =
            $"── Celkové skóre ──\n" +
            $"Hráč 1:  {data.player1Wins} výher\n" +
            $"Hráč 2:  {data.player2Wins} výher\n\n" +
            $"── Poslední zápasy ──\n";

        int start = Mathf.Max(0, data.history.Count - 5);
        for (int i = start; i < data.history.Count; i++)
        {
            var match = data.history[i];
            scoreText.text += $"{match.date}  –  Hráč {match.winner} vyhrál\n";
        }
    }
}