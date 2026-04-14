using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("NastavenÌ hry")]
    public int mousePerTeam = 2;

    public List<MouseController> Team1 { get; private set; } = new();
    public List<MouseController> Team2 { get; private set; } = new();

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
        {
            Debug.Log("Vyhr·l Hr·Ë 2!");
            // TODO: zobrazit EndScreen
        }
        else if (Team2.Count == 0)
        {
            Debug.Log("Vyhr·l Hr·Ë 1!");
            // TODO: zobrazit EndScreen
        }
    }
}