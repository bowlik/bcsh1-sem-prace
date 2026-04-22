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

        Debug.Log($"Naèítám scénu: {scene}");
        SceneManager.LoadScene(scene);
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
        Debug.Log("Hra ukonèena");
    }

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