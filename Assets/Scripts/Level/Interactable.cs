using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] public bool _isLock;
    [SerializeField] public Lock _lock;

    public bool IsLock => _isLock;
    public Key Key => _lock.Key;

    protected virtual void Awake()
    {
        if (_lock == null || _lock.Key == null)
            _isLock = false;

        _lock.gameObject.SetActive(_isLock);
    }

    public abstract void Interact();

    public void Unlock(Key key)
    {
        if (key == Key)
        {
            _isLock = false;
            _lock.gameObject.SetActive(false);
        }
    }
}