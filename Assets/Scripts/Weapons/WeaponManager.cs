using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Zbranì")]
    public WeaponBase[] weapons;

    private int _currentIndex = 0;
    private MouseController _owner;

    public void Initialize(MouseController owner)
    {
        _owner = owner;
        foreach (var w in weapons)
            w.Initialize(owner);

        SelectWeapon(0);
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
            w.enabled = false;

        // zapni vybranou
        _currentIndex = index;
        weapons[_currentIndex].enabled = true;

        Debug.Log($"Zvolena zbraò: {weapons[_currentIndex].weaponName}");
    }
}