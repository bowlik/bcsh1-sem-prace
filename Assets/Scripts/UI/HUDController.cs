using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("UI prvky")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI weaponText;

    private void Update()
    {
        if (TurnManager.Instance == null) return;

        // èasovaè
        float t = TurnManager.Instance.TimeLeft;
        timerText.text = Mathf.CeilToInt(t).ToString();
        timerText.color = t <= 5f ? Color.red : Color.white;

        // kdo hraje
        turnText.text = $"Hráè {TurnManager.Instance.CurrentTeam}";

        // aktivní zbraò
        var activeMouse = TurnManager.Instance.ActiveMouse;
        if (activeMouse != null)
        {
            var wm = activeMouse.GetComponent<WeaponManager>();
            // TODO: zobrazit název zbranì
        }
    }
}