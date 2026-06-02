using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreenController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI statsText;

    private void Start()
    {
        ShowResult();
    }

    private void ShowResult()
    {
        int winner = GameResult.WinnerTeam;
        int rounds = GameResult.RoundsPlayed;

        if (winnerText != null)
        {
            winnerText.text = winner == 0
                ? "Remiza!"
                : $"Vyhral Hrac {winner}!";

            winnerText.color = winner == 1
                ? new Color(0.3f, 0.7f, 1f)
                : new Color(1f, 0.4f, 0.4f);
        }

        if (statsText != null)
        {
            var data = ScoreManager.Instance?.GetData();
            statsText.text =
                $"Kol odehrano: {rounds}\n\n" +
                $"── Celkove skore ──\n" +
                $"Hrac 1:  {data?.player1Wins ?? 0} vyher\n" +
                $"Hrac 2:  {data?.player2Wins ?? 0} vyher\n\n" +
                $"── Posledni zapasy ──";

            if (data != null && data.history.Count > 0)
            {
                int start = Mathf.Max(0, data.history.Count - 3);
                for (int i = start; i < data.history.Count; i++)
                {
                    var match = data.history[i];
                    statsText.text += $"\n{match.date}  Hrac {match.winner} vyhral";
                }
            }
            else
            {
                statsText.text += "\nZadne predchozi zapasy";
            }
        }
    }

    public void OnPlayAgainClicked()
    {
        string scene = MapSelector.SelectedScene;
        if (string.IsNullOrEmpty(scene))
            scene = "GameScene_Goldmine";
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void OnMenuClicked()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnResetScoreClicked()
    {
        ScoreManager.Instance?.ResetScore();
        ShowResult();
    }
}