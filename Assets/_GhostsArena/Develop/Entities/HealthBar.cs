using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _barImage;

    private IDamagable _damagable;
    private float _maxWidth;
    private float _healthWidthKoef;

    public void Initialize(IDamagable damagable)
    {
        _damagable = damagable;

        _maxWidth = _barImage.rectTransform.rect.width;
        _healthWidthKoef = _maxWidth / _damagable.MaxHealth;

        _damagable.Hit += RecalculateBarWidth;
    }

    public void RecalculateBarWidth()
         => _barImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _damagable.Health * _healthWidthKoef);

    private void OnDestroy()
    {
        _damagable.Hit -= RecalculateBarWidth;
    }
}