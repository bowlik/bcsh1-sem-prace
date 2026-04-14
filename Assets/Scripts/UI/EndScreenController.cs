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
                ? "Remíza!"
                : $"Vyhrál Hráč {winner}! 🎉";

            winnerText.color = winner == 1
                ? new Color(0.3f, 0.7f, 1f)   // modrá pro hráče 1
                : new Color(1f, 0.4f, 0.4f);   // červená pro hráče 2
        }

        if (statsText != null)
        {
            statsText.text = $"Počet kol: {rounds}";

            if (ScoreManager.Instance != null)
            {
                var data = ScoreManager.Instance.GetData();
                statsText.text +=
                    $"\n\nCelkové skóre:\n" +
                    $"Hráč 1: {data.player1Wins} výher\n" +
                    $"Hráč 2: {data.player2Wins} výher";
            }
        }
    }

    public void OnPlayAgainClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}