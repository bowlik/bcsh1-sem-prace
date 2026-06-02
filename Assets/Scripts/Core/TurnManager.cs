using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    [Header("Nastavení tahu")]
    public float turnDuration = 30f;

    private int _currentTeam = 1;
    private int _currentMouseIndex = 0;
    private float _timeLeft;
    private bool _turnActive = false;
    private bool _endingTurn = false;
    private bool _isGameOver = false;
    private float _endTurnTimer = -1f;

    public MouseController ActiveMouse { get; private set; }
    public float TimeLeft => _timeLeft;
    public int CurrentTeam => _currentTeam;

    private void Awake()
    {
        // nová scéna = nová instance vždy vyhraje
        if (Instance != null && Instance != this)
            Instance = null;

        Instance = this;
        _currentTeam = 1;
        _currentMouseIndex = 0;
        _turnActive = false;
        _endingTurn = false;
        _isGameOver = false;
        _endTurnTimer = -1f;
        ActiveMouse = null;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Start()
    {
        StartCoroutine(StartTurnDelayed());
    }

    private IEnumerator StartTurnDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        if (_isGameOver) yield break;
        if (GameManager.Instance == null) yield break;
        StartTurn();
    }

    private void Update()
    {
        if (_isGameOver) return;

        if (_turnActive)
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0f)
                EndTurn();
        }

        if (_endTurnTimer > 0f)
        {
            _endTurnTimer -= Time.deltaTime;
            if (_endTurnTimer <= 0f)
            {
                _endTurnTimer = -1f;
                ExecuteNextTurn();
            }
        }
    }

    public void StartTurn()
    {
        if (_isGameOver) return;
        _endingTurn = false;

        if (GameManager.Instance == null) return;

        var team = _currentTeam == 1
            ? GameManager.Instance.Team1
            : GameManager.Instance.Team2;

        if (team == null || team.Count == 0) return;

        _currentMouseIndex %= team.Count;
        ActiveMouse = team[_currentMouseIndex];

        if (ActiveMouse == null)
        {
            team.RemoveAt(_currentMouseIndex);
            return;
        }

        ActiveMouse.SetActive(true);

        var wm = ActiveMouse.GetComponent<WeaponManager>();
        if (wm != null)
            wm.ResetWeapons();

        _timeLeft = turnDuration;
        _turnActive = true;

        GameManager.Instance._roundCount++;
        Debug.Log($"Tah hráèe {_currentTeam} – myš {_currentMouseIndex + 1}");
    }

    public void EndTurn()
    {
        if (_endingTurn || _isGameOver) return;
        _endingTurn = true;
        _turnActive = false;

        if (ActiveMouse != null)
            ActiveMouse.SetActive(false);

        _endTurnTimer = 1.5f;
    }

    private void ExecuteNextTurn()
    {
        if (_isGameOver) return;
        if (GameManager.Instance == null) return;

        if (_currentTeam == 1)
            _currentTeam = 2;
        else
        {
            _currentTeam = 1;
            _currentMouseIndex++;
        }

        StartTurn();
    }

    public void SetGameOver()
    {
        _isGameOver = true;
        _turnActive = false;
        _endTurnTimer = -1f;
        StopAllCoroutines();
    }
}