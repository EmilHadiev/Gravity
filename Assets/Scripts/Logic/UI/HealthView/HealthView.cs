using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRotator))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Color _fullHealth;
    [SerializeField] private Color _mediuamHealth;
    [SerializeField] private Color _lowHealth;

    private IHealth _health;
    private HealthColorGetter _colorGetter;

    private void Awake()
    {
        _health = GetComponentInParent<IHealth>();
        _colorGetter = new HealthColorGetter(_fullHealth, _mediuamHealth, _lowHealth);
    }

    private void OnEnable() => _health.HealthChanged += OnHealthChanged;

    private void OnDisable() => _health.HealthChanged -= OnHealthChanged;

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        float healthPercent = currentHealth / maxHealth;
        _fillImage.fillAmount = healthPercent;
        _fillImage.color = _colorGetter.GetColor(healthPercent);
    }

    private class HealthColorGetter
    {
        private const float MediumHealth = 0.5f;
        private const float LowHealth = 0.25f;

        private readonly Color _fullHealth;
        private readonly Color _mediuamHealth;
        private readonly Color _lowHealth;

        public HealthColorGetter(Color fullHealth, Color mediuamHealth, Color lowHealth)
        {
            _fullHealth = fullHealth;
            _mediuamHealth = mediuamHealth;
            _lowHealth = lowHealth;
        }

        public Color GetColor(float healthPercent)
        {
            if (healthPercent > MediumHealth)
                return _fullHealth;

            if (healthPercent <= MediumHealth && healthPercent >= LowHealth)
                return _mediuamHealth;

            return _lowHealth;
        }
    }
}