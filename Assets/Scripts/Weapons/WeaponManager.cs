using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Zbranì")]
    public WeaponBase[] weapons;

    private int _currentIndex = 0;
    private MouseController _owner;

    public int CurrentIndex => _currentIndex;

    public WeaponBase GetCurrentWeapon()
    {
        if (weapons == null || weapons.Length == 0) return null;
        return weapons[_currentIndex];
    }

    public void Initialize(MouseController owner)
    {
        _owner = owner;
        foreach (var w in weapons)
        {
            if (w != null)
            {
                w.Initialize(owner);
                w.enabled = false;
            }
        }
        SelectWeapon(0);
    }

    public void ResetWeapons()
    {
        foreach (var w in weapons)
        {
            if (w != null)
            {
                w.enabled = false;
                w.ResetFired();
            }
        }
        SelectWeapon(_currentIndex);
    }

    private void Update()
    {
        if (_owner == null || !_owner.IsActive) return;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                SelectWeapon(i);
        }
    }

    private void SelectWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        foreach (var w in weapons)
        {
            if (w != null)
                w.enabled = false;
        }

        _currentIndex = index;

        if (weapons[_currentIndex] != null)
        {
            weapons[_currentIndex].enabled = true;
            Debug.Log($"Zvolena zbraò: {weapons[_currentIndex].weaponName}");
        }
    }
}