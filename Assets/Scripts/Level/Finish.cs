using System;
using System.Linq;
using UnityEngine;

public class Finish : Interactable
{
    [SerializeField] private Switch[] _switches;
    [SerializeField] private Sprite _switchIcon;

    public event Action Activated;

    public override void Interact()
    {
        if (IsLock)
        {
            base.Interact();
            return;
        }

        if (_switches.All(i => i.IsActive))
        {
            Activated?.Invoke();
        }
        else
        {
            ShowMessage(_switches.Count(i => i.IsActive), _switches.Length, _switchIcon);
        }
    }
}
