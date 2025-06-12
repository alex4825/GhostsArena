using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _barImage;

    private IHealthable _healthable;
    private float _maxWidth;
    private float _healthWidthKoef;

    private bool _isInitiated;
    private float _currentHealth;

    public void Initiate(IHealthable healthable)
    {
        _healthable = healthable;

        _maxWidth = _barImage.rectTransform.rect.width;
        _healthWidthKoef = _maxWidth / _healthable.MaxHealth;

        _isInitiated = true;
        _currentHealth = _healthable.Health;
    }

    private void Update()
    {
        if (_isInitiated && _healthable.Health != _currentHealth)
        {
            _currentHealth = _healthable.Health;
            RecalculateBarWidth();
        }
    }

    public void RecalculateBarWidth()
         => _barImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _healthable.Health * _healthWidthKoef);
}