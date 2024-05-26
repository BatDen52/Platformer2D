using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _view;

    private Health _health;

    private void OnDestroy()
    {
        _health.ValueChanged -= OnValueChanged;
    }

    public void Initialize(Health health)
    {
        _health = health;
        _health.ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(float value, float maxValue)
    {
        _view.value = value / maxValue;
    }
}
