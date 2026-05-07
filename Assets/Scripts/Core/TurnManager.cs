using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    [Header("NastavenÌ tahu")]
    public float turnDuration = 30f;

    private int _currentTeam = 1;
    private int _currentMouseIndex = 0;
    private float _timeLeft;
    private bool _turnActive = false;
    private bool _endingTurn = false;

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
        _endingTurn = false;

        var team = _currentTeam == 1
            ? GameManager.Instance.Team1
            : GameManager.Instance.Team2;

        if (team.Count == 0) return;

        _currentMouseIndex %= team.Count;
        ActiveMouse = team[_currentMouseIndex];
        ActiveMouse.SetActive(true);

        // resetuj zbranÏ aktivnÌ myöi
        var wm = ActiveMouse.GetComponent<WeaponManager>();
        if (wm != null)
            wm.ResetWeapons();

        _timeLeft = turnDuration;
        _turnActive = true;

        GameManager.Instance._roundCount++;
        Debug.Log($"Tah hr·Ëe {_currentTeam} ñ myö {_currentMouseIndex + 1}");
    }

    public void EndTurn()
    {
        if (_endingTurn) return;
        _endingTurn = true;
        _turnActive = false;

        if (ActiveMouse != null)
            ActiveMouse.SetActive(false);

        StartCoroutine(EndTurnDelayed());
    }

    private IEnumerator EndTurnDelayed()
    {
        yield return new WaitForSeconds(1.5f);

        if (_currentTeam == 1)
            _currentTeam = 2;
        else
        {
            _currentTeam = 1;
            _currentMouseIndex++;
        }

        StartTurn();
    }
}