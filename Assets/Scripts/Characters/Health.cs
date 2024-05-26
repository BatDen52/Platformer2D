using System;
using UnityEngine;

public class Health
{
    public Health(int maxValue)
    {
        MaxValue = maxValue;
        Value = maxValue;
    }

    public event Action<float, float> ValueChanged;
    public event Action TakingDamage;
    public event Action Died;

    public int MaxValue { get; private set; }
    public int Value { get; private set; }

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            return;

        ChangeValue(-damage);

        TakingDamage?.Invoke();

        if(Value == 0) 
            Died?.Invoke();
    }

    public void Heal(int value)
    {
        if (value < 0)
            return;

        ChangeValue(value);
    }


    private void ChangeValue(int value)
    {
        Value = Mathf.Clamp(Value + value, 0, MaxValue);
        ValueChanged?.Invoke(Value, MaxValue);
    }
}
