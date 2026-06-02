using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class MatchResult
{
    public int winner;
    public string date;
    public int roundsPlayed;
}

[System.Serializable]
public class ScoreData
{
    public int player1Wins;
    public int player2Wins;
    public List<MatchResult> history = new();
}

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // vytvoø automaticky pokud neexistuje
                GameObject go = new GameObject("ScoreManager");
                _instance = go.AddComponent<ScoreManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private ScoreData _data = new();
    private string _savePath;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        _savePath = Path.Combine(Application.persistentDataPath, "score.json");
        Load();
    }

    public void SaveResult(int winnerTeam, int rounds)
    {
        if (winnerTeam == 1) _data.player1Wins++;
        else _data.player2Wins++;

        _data.history.Add(new MatchResult
        {
            winner = winnerTeam,
            date = System.DateTime.Now.ToString("dd.MM.yyyy HH:mm"),
            roundsPlayed = rounds
        });

        Save();
    }

    public void ResetScore()
    {
        _data = new ScoreData();
        Save();
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(_data, true);
        File.WriteAllText(_savePath, json);
    }

    private void Load()
    {
        if (!File.Exists(_savePath)) return;
        string json = File.ReadAllText(_savePath);
        _data = JsonUtility.FromJson<ScoreData>(json);
        Debug.Log($"Skóre naèteno – Hráè 1: {_data.player1Wins}, Hráè 2: {_data.player2Wins}");
    }

    public ScoreData GetData() => _data;
}