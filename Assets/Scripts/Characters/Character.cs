using System;
using UnityEngine;

[RequireComponent (typeof(Fliper))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private Transform _view;
    [SerializeField] private HealthBar _healthBar;

    public event Action Died;

    protected Fliper Fliper { get; private set; }
    protected Health Health { get; private set; }
    protected Transform View => _view;   

    protected virtual void Awake()
    {
        Health = new Health(_maxHealth);
        _healthBar.Initialize(Health);

        Fliper = GetComponent<Fliper>();
        Fliper.Initialize(_view);
    }

    protected virtual void OnEnable()
    {
        Health.TakingDamage += OnTakingDamage;
        Health.Died += OnDied;
    }

    protected virtual void OnDisable()
    {
        Health.TakingDamage -= OnTakingDamage;
        Health.Died -= OnDied;
    }

    public void ApplyDamage(int damage)
    {
        Health.ApplyDamage(damage);
    }

    public void Heal(int value)
    {
        Health.Heal(value);
    }

    protected virtual void OnDied()
    {
        Died?.Invoke();
    }

    protected abstract void OnTakingDamage();
}
