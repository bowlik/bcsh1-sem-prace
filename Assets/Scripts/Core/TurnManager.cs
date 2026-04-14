using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    [Header("Nastavení tahu")]
    public float turnDuration = 30f;

    private int _currentTeam = 1;
    private int _currentMouseIndex = 0;
    private float _timeLeft;
    private bool _turnActive = false;

    public MouseController ActiveMouse { get; private set; }
    public float TimeLeft => _timeLeft;
    public int CurrentTeam => _currentTeam;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartTurnDelayed());
    }

    private IEnumerator StartTurnDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        StartTurn();
    }

    private void Update()
    {
        if (!_turnActive) return;

        _timeLeft -= Time.deltaTime;
        if (_timeLeft <= 0f)
            EndTurn();
    }

    public void StartTurn()
    {
        var team = _currentTeam == 1
            ? GameManager.Instance.Team1
            : GameManager.Instance.Team2;

        if (team.Count == 0) return;

        _currentMouseIndex %= team.Count;
        ActiveMouse = team[_currentMouseIndex];
        ActiveMouse.SetActive(true);

        _timeLeft = turnDuration;
        _turnActive = true;

        Debug.Log($"Tah hráèe {_currentTeam} – myš {_currentMouseIndex + 1}");
    }

    public void EndTurn()
    {
        _turnActive = false;

        if (ActiveMouse != null)
            ActiveMouse.SetActive(false);

        // pøepni tým a posuò index myší
        if (_currentTeam == 1)
        {
            _currentTeam = 2;
        }
        else
        {
            _currentTeam = 1;
            _currentMouseIndex++;
        }

        StartTurn();
    }
}