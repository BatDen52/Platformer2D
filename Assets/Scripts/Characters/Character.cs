using System;
using UnityEngine;

[RequireComponent (typeof(Fliper))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private Transform _view;
    [SerializeField] private HealthBar _healthBar;

    private Health _health;
    private Fliper _fliper;

    public event Action Died;

    protected Fliper Fliper => _fliper; 

    protected virtual void Awake()
    {
        _health = new Health(_maxHealth);
        _healthBar.Initialize(_health);

        _fliper = GetComponent<Fliper>();
        _fliper.Initialize(_view);
    }

    protected virtual void OnEnable()
    {
        _health.TakingDamage += OnTakingDamage;
        _health.Died += OnDied;
    }

    protected virtual void OnDisable()
    {
        _health.TakingDamage -= OnTakingDamage;
        _health.Died -= OnDied;
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);
    }

    public void Heal(int value)
    {
        _health.Heal(value);
    }

    protected virtual void OnDied()
    {
        Died?.Invoke();
    }

    protected abstract void OnTakingDamage();
}
