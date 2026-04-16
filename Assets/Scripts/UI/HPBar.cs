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

        // nastav výplò podle HP
        float ratio = (float)_owner.currentHp / _owner.maxHp;
        fillImage.fillAmount = ratio;

        // barva podle HP
        fillImage.color = Color.Lerp(Color.red, Color.green, ratio);

        // HP bar vždy smìøuje na kameru
        transform.rotation = Camera.main.transform.rotation;
    }
}