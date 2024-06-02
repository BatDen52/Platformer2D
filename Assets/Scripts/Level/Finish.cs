using System;
using System.Linq;
using UnityEngine;

public class Finish : Interactable
{
    [SerializeField] public Switch[] _switches;

    public event Action Activated;

    public override void Interact()
    {
        if (_isLock)
            return;

        if (_switches.All(i => i.IsActive))
        {
            Activated?.Invoke();
        }
    }
}
