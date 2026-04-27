using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Nastavení hry")]
    public int mousePerTeam = 2;

    public List<MouseController> Team1 { get; private set; } = new();
    public List<MouseController> Team2 { get; private set; } = new();

    public int _roundCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterMouse(MouseController mouse, int team)
    {
        if (team == 1) Team1.Add(mouse);
        else Team2.Add(mouse);
    }

    public void OnMouseDied(MouseController mouse)
    {
        Team1.Remove(mouse);
        Team2.Remove(mouse);

        if (Team1.Count == 0)
            EndGame(2);
        else if (Team2.Count == 0)
            EndGame(1);
    }

    private void EndGame(int winner)
    {
        Debug.Log($"Vyhrál Hráè {winner}!");

        GameResult.WinnerTeam = winner;
        GameResult.RoundsPlayed = _roundCount;

        ScoreManager.Instance?.SaveResult(winner, _roundCount);

        StartCoroutine(LoadEndScreen());
    }

    private IEnumerator LoadEndScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndScene");
    }
}