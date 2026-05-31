using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image fillImage;
    private MouseController _owner;

    public void Initialize(MouseController owner)
    {
        _owner = owner;
    }

    private void Update()
    {
        if (_owner == null) return;

        float ratio = (float)_owner.currentHp / _owner.maxHp;
        fillImage.fillAmount = ratio;

        // zelená → žlutá → červená podle HP
        if (ratio > 0.5f)
            fillImage.color = Color.green;
        else if (ratio > 0.25f)
            fillImage.color = Color.yellow;
        else
            fillImage.color = Color.red;

        // HP bar vždy směřuje na kameru
        transform.rotation = Camera.main.transform.rotation;
    }
}