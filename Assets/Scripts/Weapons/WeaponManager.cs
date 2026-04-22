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
                w.enabled = false; // vypni všechny na zaèátku
            }
        }

        SelectWeapon(0); // zapni první zbraò
    }

    private void Update()
    {
        if (_owner == null || !_owner.IsActive) return;

        // pøepínání zbraní klávesami 1–7
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                SelectWeapon(i);
        }
    }

    private void SelectWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        // vypni všechny
        foreach (var w in weapons)
        {
            if (w != null)
                w.enabled = false;
        }

        // zapni vybranou
        _currentIndex = index;

        if (weapons[_currentIndex] != null)
        {
            weapons[_currentIndex].enabled = true;
            Debug.Log($"Zvolena zbraò: {weapons[_currentIndex].weaponName}");
        }
    }
}